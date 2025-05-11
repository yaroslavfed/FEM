using System.Diagnostics.CodeAnalysis;
using FEM.Server.Data;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Services.AssemblyService;
using FEM.Server.Services.BoundaryConditionService;
using FEM.Server.Services.SolutionExportService;
using FEM.Server.Services.SourceProvider;
using FEM.Server.Services.TestSessionService;
using FEM.Server.Services.VisualizerService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using FEM.Server.Services.PlotService;
using FEM.Server.Services.SensorEvaluator;
using FEM.Server.Services.SensorGenerator;

namespace FEM.Server.Controllers;

/// <summary>
/// Контроллер для решения векторного МКЭ
/// </summary>
[ApiController]
[Route("api/[controller]/Vector")]
public class FemController : ControllerBase
{
    private readonly ITestSessionService       _testSessionService;
    private readonly ICurrentSourceProvider    _currentSourceProvider;
    private readonly IVisualizerService        _visualizerService;
    private readonly IAssemblyService          _assemblyService;
    private readonly IBoundaryConditionService _boundaryConditionService;
    private readonly ISolutionExportService    _solutionExportService;
    private readonly IPlotService              _plotService;
    private readonly ISensorEvaluator          _sensorEvaluator;

    /// <inheritdoc />
    public FemController(
        ITestSessionService testSessionService,
        ICurrentSourceProvider currentSourceProvider,
        IVisualizerService visualizerService,
        IAssemblyService assemblyService,
        IBoundaryConditionService boundaryConditionService,
        ISolutionExportService solutionExportService,
        IPlotService plotService,
        ISensorEvaluator sensorEvaluator
    )
    {
        _testSessionService = testSessionService;
        _currentSourceProvider = currentSourceProvider;
        _assemblyService = assemblyService;
        _boundaryConditionService = boundaryConditionService;
        _solutionExportService = solutionExportService;
        _plotService = plotService;
        _sensorEvaluator = sensorEvaluator;
        _visualizerService = visualizerService;
    }

    /// <summary>
    /// Решает уравнение с помощью векторного МКЭ
    /// </summary>
    /// <param name="testSessionParameters">Входные параметры расчётной сессии</param>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     POST
    ///     {
    ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///         "meshParameters": {
    ///             "xCenterCoordinate": 0,
    ///             "yCenterCoordinate": 0,
    ///             "zCenterCoordinate": -1,
    ///             "xStepToBounds": 2,
    ///             "yStepToBounds": 2,
    ///             "zStepToBounds": 2
    ///         },
    ///         "splittingParameters": {
    ///             "xSplittingCoefficient": 4,
    ///             "ySplittingCoefficient": 4,
    ///             "zSplittingCoefficient": 4,
    ///             "xMultiplyCoefficient": 1,
    ///             "yMultiplyCoefficient": 1,
    ///             "zMultiplyCoefficient": 1
    ///         },
    ///         "additionParameters": {
    ///             "muCoefficient": 1,
    ///             "gammaCoefficient": 1,
    ///             "boundaryCondition": 0
    ///         },
    ///         "strataList": [
    ///         {
    ///             "positioning": {
    ///                 "centerCoordinate": {
    ///                     "x": 0,
    ///                     "y": 0,
    ///                     "z": 0
    ///                 },
    ///                 "boundsDistance": {
    ///                     "x": 1,
    ///                     "y": 1,
    ///                     "z": 1
    ///                 }
    ///             },
    ///             "mu": 6
    ///         }
    ///         ],
    ///         "currentSource": {
    ///             "start": {
    ///                 "x": -1,
    ///                 "y": 0,
    ///                 "z": 0
    ///             },
    ///             "end": {
    ///                 "x": 1,
    ///                 "y": 0,
    ///                 "z": 0
    ///             },
    ///             "amperage": 100,
    ///             "segments": 10
    ///         }
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Возвращает id результата, невязку и количество итераций</response>
    /// <response code="500">На сервере что-то пошло не так</response>
    /// <returns>Решение уравнения</returns>
    [HttpPost(Name = "vector-fem-solver")]
    [ProducesResponseType(typeof(FemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SuppressMessage("ReSharper.DPA", "DPA0011: High execution time of MVC action")]
    public async Task<IActionResult> CreateCalculation([FromBody] TestSession testSessionParameters)
    {
        Console.WriteLine($"[Started session] Id: {testSessionParameters.Id}");
        try
        {
            testSessionParameters.Sensors = SensorGenerator.GenerateXYPlaneSensors(
                xMin: -5,
                xMax: 5,
                xCount: 11,
                yMin: -5,
                yMax: 5,
                yCount: 11,
                zLevel: 0.0,
                component: ESensorComponent.Bz
            );

            // 1. Построение сетки
            var testSession = await _testSessionService.CreateTestSessionAsync(testSessionParameters);
            await _visualizerService.DrawMeshPlotAsync(testSession.Mesh);

            await _plotService.ShowPlotAsync(testSession.Mesh);

            // 3. Источник тока
            var sources = await _currentSourceProvider.GetSourcesAsync(testSessionParameters.CurrentSource);

            // 5. Построение матрицы жесткости и вектора правой части
            // Собираем глобальный список уникальных рёбер по EdgeIndex
            var (matrix, rhs) = await _assemblyService.AssembleGlobalSystemAsync(testSession.Mesh, sources);

            Console.WriteLine($"5.\trhs.Size {rhs.Size}\trhs.Min {rhs.Min()}\trhs.Max {rhs.Max()}");

            // 6. Применение краевых условий
            var constrainedDofs = ComputeBoundaryEdgeIndices(testSession.Mesh);
            await _boundaryConditionService.ApplyBoundaryConditionsAsync(matrix, rhs, constrainedDofs);

            Console.WriteLine($"6.\trhs.Size {rhs.Size}\trhs.Min {rhs.Min()}\trhs.Max {rhs.Max()}");

            // 7. Решение СЛАУ
            // ReSharper disable once InconsistentNaming
            var A = matrix.ToMathNet();
            var b = rhs.ToMathNet();

            Console.WriteLine($"7.\tb.Size {b.Count}\tb.Min {b.Min()}\tb.Max {b.Max()}");

            // Выполняем LU-разложение
            var solver = A.LU();

            // Решаем СЛАУ
            var x = solver.Solve(b);

            // Конвертируем обратно в твой Vector
            var solution = Vector.FromMathNet(x);
            Console.WriteLine(
                $"8.\tsolution.Size {solution.Size}\tsolution.Min {solution.Min()}\tsolution.Max {solution.Max()}"
            );
            Console.WriteLine(solution.ToString());

            _solutionExportService.ExportSensorsToJson(
                testSessionParameters.Sensors,
                testSession.Mesh,
                solution,
                "bfield_3d.json"
            );

            var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts\\visualize_sensors.py");
            using Process myProcess = new();
            myProcess.StartInfo.FileName = "python";
            myProcess.StartInfo.Arguments = scriptPath;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.RedirectStandardInput = true;
            myProcess.StartInfo.RedirectStandardOutput = false;
            myProcess.Start();

            return Ok(solution);
        } catch (Exception exception)
        {
            return BadRequest(
                $"Something went wrong. Status code: {StatusCodes.Status500InternalServerError}, {exception.Message}"
            );
        } finally
        {
            Console.WriteLine($"[Ended session] Id: {testSessionParameters.Id}");
        }
    }

    private static List<int> ComputeBoundaryEdgeIndices(Mesh mesh)
    {
        var edgeUsageCount = new Dictionary<int, int>();

        foreach (var element in mesh.Elements)
        {
            foreach (var edge in element.Edges)
            {
                edgeUsageCount.TryAdd(edge.EdgeIndex, 0);

                edgeUsageCount[edge.EdgeIndex]++;
            }
        }

        // Рёбра, которые встречаются только один раз — это граничные
        var boundaryEdges = edgeUsageCount.Where(kvp => kvp.Value == 1).Select(kvp => kvp.Key).ToList();

        return boundaryEdges;
    }

}
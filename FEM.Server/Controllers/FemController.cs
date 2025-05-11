using System.Diagnostics.CodeAnalysis;
using FEM.Server.Data;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Services.AssemblyService;
using FEM.Server.Services.BoundaryConditionService;
using FEM.Server.Services.SourceProvider;
using FEM.Server.Services.TestSessionService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using FEM.Common.Data.MathModels;
using FEM.Server.Services.BasisFunctionProvider;
using FEM.Server.Services.PlotService;
using FEM.Server.Services.SensorGenerator;
using MathNet.Numerics.LinearAlgebra.Double;
using Vector = FEM.Server.Data.Domain.Vector;

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
    private readonly IAssemblyService          _assemblyService;
    private readonly IBoundaryConditionService _boundaryConditionService;
    private readonly IPlotService              _plotService;
    private readonly IBasisFunctionProvider    _basisFunctionProvider;

    /// <inheritdoc />
    public FemController(
        ITestSessionService testSessionService,
        ICurrentSourceProvider currentSourceProvider,
        IAssemblyService assemblyService,
        IBoundaryConditionService boundaryConditionService,
        IPlotService plotService,
        IBasisFunctionProvider basisFunctionProvider
    )
    {
        _testSessionService = testSessionService;
        _currentSourceProvider = currentSourceProvider;
        _assemblyService = assemblyService;
        _boundaryConditionService = boundaryConditionService;
        _plotService = plotService;
        _basisFunctionProvider = basisFunctionProvider;
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
    ///             "zCenterCoordinate": -200,
    ///             "xStepToBounds": 250,
    ///             "yStepToBounds": 250,
    ///             "zStepToBounds": 100
    ///         },
    ///         "splittingParameters": {
    ///             "xSplittingCoefficient": 10,
    ///             "ySplittingCoefficient": 10,
    ///             "zSplittingCoefficient": 6,
    ///             "xMultiplyCoefficient": 1,
    ///             "yMultiplyCoefficient": 1,
    ///             "zMultiplyCoefficient": 1
    ///         },
    ///         "additionParameters": {
    ///             "muCoefficient": 1.2566e-6,
    ///             "gammaCoefficient": 1,
    ///             "boundaryCondition": 0
    ///         },
    ///         "strataList": [
    ///         {
    ///             "positioning": {
    ///                 "centerCoordinate": {
    ///                     "x": 0,
    ///                     "y": 0,
    ///                     "z": -200
    ///                 },
    ///                 "boundsDistance": {
    ///                     "x": 50,
    ///                     "y": 100,
    ///                     "z": 100
    ///                 }
    ///             },
    ///             "mu": 1.2566e-3
    ///         }
    ///         ],
    ///         "currentSource": {
    ///             "start": {
    ///                 "x": 0,
    ///                 "y": -25,
    ///                 "z": 0
    ///             },
    ///             "end": {
    ///                 "x": 0,
    ///                 "y": 25,
    ///                 "z": 0
    ///             },
    ///             "amperage": 10,
    ///             "segments": 100 
    ///         },
    ///         "sensors": [
    ///         {
    ///             "position": {
    ///                 "x": 0,
    ///                 "y": 0,
    ///                 "z": 0
    ///             },
    ///             "componentDirection": "Bx"
    ///         }
    ///         ]
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
            var sensors = testSessionParameters.Sensors = SensorGenerator.GenerateXYPlaneSensors(
                xMin: -100,
                xMax: 100,
                xCount: 30,
                yMin: -100,
                yMax: 100,
                yCount: 30,
                zLevel: 0,
                component: ESensorComponent.Bz
            );

            // 1. Построение сетки
            var testSession = await _testSessionService.CreateTestSessionAsync(testSessionParameters);
            await _plotService.ShowPlotAsync(testSession.Mesh, sensors);

            // 3. Источник тока
            var sources = await _currentSourceProvider.GetSourcesAsync(testSessionParameters.CurrentSource);

            // 5. Построение матрицы жесткости и вектора правой части
            var (globalMatrix, globalRhs) = await _assemblyService.AssembleGlobalSystemAsync(testSession.Mesh, sources);

            Console.WriteLine(
                $"5.\tglobalMatrix.Size (globalMatrix.Rows {globalMatrix.Rows} * globalMatrix.Columns {globalMatrix.Columns} = {globalMatrix.Rows * globalMatrix.Columns})\tglobalMatrix.Min {globalMatrix.Min()}\tglobalMatrix.Max {globalMatrix.Max()}"
            );

            Console.WriteLine(
                $"5.\tglobalRhs.Size {globalRhs.Size}\tglobalRhs.Min {globalRhs.Min()}\tglobalRhs.Max {globalRhs.Max()}"
            );

            // 6. Применение краевых условий
            var constrainedDofs = ComputeBoundaryEdgeIndices(testSession.Mesh);
            await _boundaryConditionService.ApplyBoundaryConditionsAsync(globalMatrix, globalRhs, constrainedDofs);

            Console.WriteLine($"6.\tConstrained DOFs: {constrainedDofs.Count} / {globalMatrix.Rows}");

            // 7. Решение СЛАУ
            // ReSharper disable once InconsistentNaming
            var A = globalMatrix.ToMathNet();
            var b = globalRhs.ToMathNet();

            Console.WriteLine(
                $"7.\tA.Size (A.Rows {A.RowCount} * A.Columns {A.ColumnCount} = {A.RowCount * A.ColumnCount})"
            );
            Console.WriteLine($"7.\tb.Size {b.Count}\tb.Min {b.Min()}\tb.Max {b.Max()}");

            // Решаем СЛАУ
            var At = A.Transpose();
            var lambda = 1e-3;
            var AtA = At * A;
            var I = DenseMatrix.CreateIdentity(A.ColumnCount);
            
            var regularized = AtA + lambda * I;
            var rhs = At * b;
            
            var x = regularized.Solve(rhs);

            var solution = Vector.FromMathNet(x);

            Console.WriteLine($"8.\tsolution.Min = {solution.Min()}, solution.Max = {solution.Max()}");

            var samples = new List<FieldSample>();

            foreach (var sensor in sensors)
            {
                var element = FindElementContaining(sensor.Position, testSession.Mesh);
                var B = ComputeMagneticFieldAt(sensor.Position, element, solution);

                samples.Add(
                    new()
                    {
                        X = sensor.Position.X,
                        Y = sensor.Position.Y,
                        Z = sensor.Position.Z,
                        Bx = B.X,
                        By = B.Y,
                        Bz = B.Z,
                        Magnitude = B.Norm()
                    }
                );
            }

            var json = JsonSerializer.Serialize(samples, new JsonSerializerOptions { WriteIndented = true });
            await System.IO.File.WriteAllTextAsync("field_data.json", json);

            var scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts\\contour_plot.py");
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

    private static List<int> ComputeBoundaryEdgeIndices(Mesh mesh, double eps = 1e-8)
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

        var allNodes = mesh
                       .Elements
                       .SelectMany(e => e.Edges)
                       .SelectMany(e => e.Nodes)
                       .DistinctBy(n => n.NodeIndex)
                       .ToList();

        var xMin = allNodes.Min(n => n.Coordinate.X);
        var xMax = allNodes.Max(n => n.Coordinate.X);
        var yMin = allNodes.Min(n => n.Coordinate.Y);
        var yMax = allNodes.Max(n => n.Coordinate.Y);
        var zMin = allNodes.Min(n => n.Coordinate.Z);
        var zMax = allNodes.Max(n => n.Coordinate.Z);

        var constrainedEdges = edgeUsageCount
                               .Where(kvp => kvp.Value == 1) // граничные рёбра
                               .Select(kvp => mesh.GetEdgeByIndex(kvp.Key))
                               .Where(edge =>
                                   {
                                       var coords = edge.Nodes.Select(n => n.Coordinate).ToList();
                                       return coords.All(c => Math.Abs(c.X - xMin) < eps
                                                              || Math.Abs(c.X - xMax) < eps
                                                              || Math.Abs(c.Y - yMin) < eps
                                                              || Math.Abs(c.Y - yMax) < eps
                                                              || Math.Abs(c.Z - zMin) < eps
                                                              || Math.Abs(c.Z - zMax) < eps
                                       );
                                   }
                               )
                               .Select(edge => edge.EdgeIndex)
                               .Distinct()
                               .ToList();

        return constrainedEdges;
    }

    private FiniteElement FindElementContaining(Point3D point, Mesh mesh)
    {
        foreach (var element in mesh.Elements)
        {
            var nodes = element.Edges.SelectMany(e => e.Nodes).DistinctBy(n => n.NodeIndex).ToList();

            var minX = nodes.Min(n => n.Coordinate.X);
            var maxX = nodes.Max(n => n.Coordinate.X);
            var minY = nodes.Min(n => n.Coordinate.Y);
            var maxY = nodes.Max(n => n.Coordinate.Y);
            var minZ = nodes.Min(n => n.Coordinate.Z);
            var maxZ = nodes.Max(n => n.Coordinate.Z);

            if (point.X >= minX
                && point.X <= maxX
                && point.Y >= minY
                && point.Y <= maxY
                && point.Z >= minZ
                && point.Z <= maxZ)
            {
                return element;
            }
        }

        throw new("Sensor is not inside any element.");
    }

    private Vector3D ComputeMagneticFieldAt(Point3D sensor, FiniteElement element, Vector solution)
    {
        Vector3D B = Vector3D.Zero;

        for (int i = 0; i < element.Edges.Count; i++)
        {
            var curlWi = _basisFunctionProvider.GetCurl(element, i, sensor);
            var dofIndex = element.Edges[i].EdgeIndex;
            var coeff = solution[dofIndex];

            B += curlWi * coeff;
        }

        return B;
    }
}
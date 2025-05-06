using System.Diagnostics.CodeAnalysis;
using FEM.Common.Enums;
using FEM.Server.Data;
using FEM.Server.Data.Domain;
using FEM.Server.Services.AssemblyService;
using FEM.Server.Services.BoundaryConditionService;
using FEM.Server.Services.GlobalMatrixService;
using FEM.Server.Services.IBasisFunctionProvider;
using FEM.Server.Services.InaccuracyService;
using FEM.Server.Services.MatrixPortraitService;
using FEM.Server.Services.PlotService;
using FEM.Server.Services.RightPartVectorService;
using FEM.Server.Services.SolverService;
using FEM.Server.Services.SourceProvider;
using FEM.Server.Services.TestResultService;
using FEM.Server.Services.TestSessionService;
using FEM.Server.Services.VisualizerService;
using Microsoft.AspNetCore.Mvc;

namespace FEM.Server.Controllers;

/// <summary>
/// Контроллер для решения векторного МКЭ
/// </summary>
[ApiController]
[Route("api/[controller]/Vector")]
public class FemController : ControllerBase
{
    private readonly ITestSessionService    _testSessionService;
    private readonly ICurrentSourceProvider _currentSourceProvider;
    private readonly IBasisFunctionProvider _basisFunctionProvider;

    public FemController(
        ITestSessionService testSessionService,
        ICurrentSourceProvider currentSourceProvider,
        IBasisFunctionProvider basisFunctionProvider
    )
    {
        _testSessionService = testSessionService;
        _currentSourceProvider = currentSourceProvider;
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
    ///             "zCenterCoordinate": 0,
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
    ///             {
    ///                 "positioning": {
    ///                     "centerCoordinate": {
    ///                         "x": 0,
    ///                         "y": 0,
    ///                         "z": 0
    ///                     },
    ///                     "boundsDistance": {
    ///                         "x": 1,
    ///                         "y": 1,
    ///                         "z": 1
    ///                     }
    ///                 },
    ///                 "mu": 6
    ///             }
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
        try
        {
            // 1. Построение сетки
            var testSession = await _testSessionService.CreateTestSessionAsync(testSessionParameters);

            // 3. Источник тока
            var sources = await _currentSourceProvider.GetSourcesAsync(testSessionParameters.CurrentSource);

            // 5. Построение матрицы жесткости и вектора правой части
            var matrix = new Matrix(mesh.Edges.Count, mesh.Edges.Count);
            var rhs = new Vector(mesh.Edges.Count);

            foreach (var element in mesh.Elements)
            {
                var localMatrix = await _problemService.GetLocalMatrixAsync(element);
                var localVector = await _problemService.GetLocalVectorAsync(element, sources);

                var globalIndices = element.GetGlobalEdgeIndices();

                matrix.Assemble(localMatrix, globalIndices);
                rhs.Assemble(localVector, globalIndices);
            }

            // 6. Применение краевых условий
            await _firstBoundaryConditionService.ApplyAsync(
                matrix,
                rhs,
                mesh,
                testSession.AdditionParameters.BoundaryCondition
            );

            // 7. Решение СЛАУ
            var solution = matrix.Solve(rhs);

            // 8. Постобработка или сохранение результата
            return Ok(new { Message = "Calculation completed successfully" });
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

}
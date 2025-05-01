using System.Diagnostics.CodeAnalysis;
using FEM.Common.Enums;
using FEM.Server.Data;
using FEM.Server.Data.Domain;
using FEM.Server.Services.InaccuracyService;
using FEM.Server.Services.Parallelepipedal.BoundaryConditionService;
using FEM.Server.Services.Parallelepipedal.GlobalMatrixService;
using FEM.Server.Services.Parallelepipedal.MatrixPortraitService;
using FEM.Server.Services.Parallelepipedal.RightPartVectorService;
using FEM.Server.Services.Parallelepipedal.VisualizerService;
using FEM.Server.Services.PlotService;
using FEM.Server.Services.SolverService;
using FEM.Server.Services.TestResultService;
using FEM.Server.Services.TestSessionService;
using Microsoft.AspNetCore.Mvc;

namespace FEM.Server.Controllers;

/// <summary>
/// Контроллер для решения векторного МКЭ
/// </summary>
[ApiController]
[Route("api/[controller]/Vector")]
public class FemController : ControllerBase
{
    private readonly IGlobalMatrixServices     _globalMatrixServices;
    private readonly ITestSessionService       _testSessionService;
    private readonly IMatrixPortraitService    _portraitService;
    private readonly IRightPartVectorService   _rightPartVectorService;
    private readonly IVisualizerService        _visualizerService;
    private readonly IBoundaryConditionFactory _boundaryCondition;
    private readonly ISolverService            _solverService;
    private readonly ITestResultService        _testResultService;
    private readonly IInaccuracyService        _inaccuracyService;
    private readonly IPlotService              _plotService;

    public FemController(
        IGlobalMatrixServices globalMatrixServices,
        ITestSessionService testSessionService,
        IMatrixPortraitService portraitService,
        IRightPartVectorService rightPartVectorService,
        IVisualizerService visualizerService,
        IBoundaryConditionFactory boundaryCondition,
        ISolverService solverService,
        ITestResultService testResultService,
        IInaccuracyService inaccuracyService,
        IPlotService plotService
    )
    {
        _globalMatrixServices = globalMatrixServices;
        _testSessionService = testSessionService;
        _portraitService = portraitService;
        _rightPartVectorService = rightPartVectorService;
        _visualizerService = visualizerService;
        _boundaryCondition = boundaryCondition;
        _solverService = solverService;
        _testResultService = testResultService;
        _inaccuracyService = inaccuracyService;
        _plotService = plotService;
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
        Console.WriteLine($"[Started session] Id: {testSessionParameters.Id}");

        try
        {
            var testSession = await _testSessionService.CreateTestSessionAsync(testSessionParameters);
            
            var matrixProfile
                = await _portraitService.ResolveMatrixPortraitAsync(testSession.Mesh, EMatrixFormats.Profile);
            var solutionParameters = await _solverService.GetSolutionVectorAsync(matrixProfile, 1000, 1e-15);
            var boundaryConditionService
                = await _boundaryCondition.ResolveBoundaryConditionAsync(testSession.BoundaryCondition);

            await matrixProfile.InitializeVectorsAsync(
                testSession
                    .Mesh
                    .Elements
                    .SelectMany(element => element.Edges)
                    .DistinctBy(edge => edge.EdgeIndex)
                    .Count()
            );

            await _globalMatrixServices.GetGlobalMatrixAsync(matrixProfile, testSession);
            await _rightPartVectorService.GetRightPartVectorAsync(matrixProfile, testSession);

            await boundaryConditionService.SetBoundaryConditionsAsync(testSession, matrixProfile);

            await _inaccuracyService.GetSolutionVectorInaccuracy(testSession, solutionParameters);

            await _visualizerService.WriteMatrixToFileAsync(matrixProfile);
            await _visualizerService.DrawMeshPlotAsync(testSession.Mesh);
            await _plotService.ShowPlotAsync(testSession.Mesh);

            var resultId = await _testResultService.AddTestResultAsync(solutionParameters);
            var result = await _testResultService.GetTestResultAsync(resultId);

            return Ok(result);
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
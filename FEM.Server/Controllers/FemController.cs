using FEM.Common.Data.TestSession;
using FEM.Common.Enums;
using FEM.Server.Data;
using FEM.Server.Data.Domain;
using FEM.Server.Services.Parallelepipedal.BoundaryConditionService;
using FEM.Server.Services.Parallelepipedal.GlobalMatrixService;
using FEM.Server.Services.Parallelepipedal.MatrixPortraitService;
using FEM.Server.Services.Parallelepipedal.RightPartVectorService;
using FEM.Server.Services.Parallelepipedal.VisualizerService;
using FEM.Server.Services.SaverService;
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
    private readonly ILogger                   _logger;
    private readonly IGlobalMatrixServices     _globalMatrixServices;
    private readonly ITestSessionService       _testSessionService;
    private readonly IMatrixPortraitService    _portraitService;
    private readonly IRightPartVectorService   _rightPartVectorService;
    private readonly IVisualizerService        _visualizerService;
    private readonly IBoundaryConditionFactory _boundaryCondition;
    private readonly ISolverService            _solverService;
    private readonly ITestResultService        _testResultService;

    public FemController(
        ILogger<FemController> logger,
        IGlobalMatrixServices globalMatrixServices,
        ITestSessionService testSessionService,
        IMatrixPortraitService portraitService,
        IRightPartVectorService rightPartVectorService,
        IVisualizerService visualizerService,
        IBoundaryConditionFactory boundaryCondition,
        ISolverService solverService,
        ITestResultService testResultService
    )
    {
        _logger = logger;
        _globalMatrixServices = globalMatrixServices;
        _testSessionService = testSessionService;
        _portraitService = portraitService;
        _rightPartVectorService = rightPartVectorService;
        _visualizerService = visualizerService;
        _boundaryCondition = boundaryCondition;
        _solverService = solverService;
        _testResultService = testResultService;
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
    ///        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///        "meshParameters": {
    ///            "xCenterCoordinate": 0.5,
    ///            "yCenterCoordinate": 0.5,
    ///            "zCenterCoordinate": 0.5,
    ///            "xStepToBounds": 0.5,
    ///            "yStepToBounds": 0.5,
    ///            "zStepToBounds": 0.5
    ///        },
    ///        "splittingParameters": {
    ///            "xSplittingCoefficient": 4,
    ///            "ySplittingCoefficient": 4,
    ///            "zSplittingCoefficient": 4,
    ///            "xMultiplyCoefficient": 1,
    ///            "yMultiplyCoefficient": 1,
    ///            "zMultiplyCoefficient": 1
    ///        },
    ///        "additionParameters": {
    ///            "muCoefficient": 1,
    ///            "gammaCoefficient": 1,
    ///            "boundaryCondition": 0
    ///        }
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Возвращает id результата, невязку и количество итераций</response>
    /// <response code="500">На сервере что-то пошло не так</response>
    /// <returns>Решение уравнения</returns>
    [HttpPost(Name = "vector-fem-solver")]
    [ProducesResponseType(typeof(FemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCalculation([FromBody] TestSession testSessionParameters)
    {
        Console.WriteLine($"[Started session] Id: {testSessionParameters.Id}");

        try
        {
            _logger.LogInformation($"[{nameof(FemController)}] {nameof(CreateCalculation)} initialize");

            _logger.LogInformation($"[{nameof(FemController)}] create test session");
            var testSession = await _testSessionService.CreateTestSessionAsync(testSessionParameters);

            _logger.LogInformation($"[{nameof(FemController)}] resolve matrix portrait");
            var matrixProfile
                = await _portraitService.ResolveMatrixPortraitAsync(testSession.Mesh, EMatrixFormats.Profile);
            await matrixProfile.InitializeVectorsAsync(
                testSession
                    .Mesh
                    .Elements
                    .SelectMany(element => element.Edges)
                    .DistinctBy(edge => edge.EdgeIndex)
                    .Count()
            );

            _logger.LogInformation($"[{nameof(FemController)}] calculate global matrix");
            await _globalMatrixServices.GetGlobalMatrixAsync(matrixProfile, testSession);

            _logger.LogInformation($"[{nameof(FemController)}] calculate right part vector");
            await _rightPartVectorService.GetRightPartVectorAsync(matrixProfile, testSession);

            _logger.LogInformation($"[{nameof(FemController)}] resolve boundary conditions");
            var boundaryConditionService
                = await _boundaryCondition.ResolveBoundaryConditionAsync(testSession.BoundaryCondition);

            _logger.LogInformation($"[{nameof(FemController)}] set boundary conditions");
            await boundaryConditionService.SetBoundaryConditionsAsync(testSession, matrixProfile);

            _logger.LogInformation($"[{nameof(FemController)}] save matrix profile to files");
            await _visualizerService.WriteMatrixToFileAsync(matrixProfile);

            _logger.LogInformation($"[{nameof(FemController)}] save plots to images");
            await _visualizerService.DrawMeshPlotAsync(testSession.Mesh);

            _logger.LogInformation($"[{nameof(FemController)}] calculate slae start");
            var solutionParameters = await _solverService.GetSolutionVectorAsync(matrixProfile, 1000, 1e-15);

            _logger.LogInformation($"[{nameof(FemController)}] saving test result");
            var resultId = await _testResultService.AddTestResultAsync(solutionParameters);

            var femResponse = new FemResponse
            {
                Id = resultId,
                Discrepancy = solutionParameters.Discrepancy,
                IterationsCount = solutionParameters.ItersCount
            };

            return Ok(femResponse);
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

    /// <summary>
    /// Получает результат сессии из хранилища
    /// </summary>
    /// <param name="id">Идентификатор проведенной расчётной сессии</param>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     Get
    ///     3fa85f64-5717-4562-b3fc-2c963f66afa6
    /// </remarks>
    /// <response code="200">Полную информацию о проведенной сессии</response>
    /// <response code="500">На сервере что-то пошло не так</response>
    /// <returns>Полная модель решения уравнения</returns>
    [HttpGet("{id:guid}", Name = "additional-info")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTestResult(Guid id)
    {
        try
        {
            var result = await _testResultService.GetTestResultAsync(id);
            return Ok(result);
        } catch (Exception exception)
        {
            return BadRequest(
                $"Something went wrong. Status code: {StatusCodes.Status500InternalServerError}, {exception.Message}"
            );
        }
    }
}
using FEM.Common.Enums;
using FEM.Server.Data;
using FEM.Server.Data.Domain;
using FEM.Server.Services.Parallelepipedal.BoundaryConditionService;
using FEM.Server.Services.Parallelepipedal.DrawingMeshService;
using FEM.Server.Services.Parallelepipedal.GlobalMatrixService;
using FEM.Server.Services.Parallelepipedal.MatrixPortraitService;
using FEM.Server.Services.Parallelepipedal.RightPartVectorService;
using FEM.Server.Services.SolverService;
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
    private readonly ILogger _logger;
    private readonly IGlobalMatrixServices _globalMatrixServices;
    private readonly ITestSessionService _testSessionService;
    private readonly IMatrixPortraitService _portraitService;
    private readonly IRightPartVectorService _rightPartVectorService;
    private readonly IVisualizerService _visualizerService;
    private readonly IBoundaryConditionFactory _boundaryCondition;
    private readonly ISolverService _solverService;

    public FemController(
        ILogger<FemController> logger,
        IGlobalMatrixServices globalMatrixServices,
        ITestSessionService testSessionService,
        IMatrixPortraitService portraitService,
        IRightPartVectorService rightPartVectorService,
        IVisualizerService visualizerService,
        IBoundaryConditionFactory boundaryCondition,
        ISolverService solverService)
    {
        _logger = logger;
        _globalMatrixServices = globalMatrixServices;
        _testSessionService = testSessionService;
        _portraitService = portraitService;
        _rightPartVectorService = rightPartVectorService;
        _visualizerService = visualizerService;
        _boundaryCondition = boundaryCondition;
        _solverService = solverService;
    }

    /// <summary>
    /// Решает уравнение с помощью векторного МКЭ
    /// </summary>
    /// <param name="testSessionParameters">Входные параметры расчётной сессии</param>
    /// /// <remarks>
    /// Sample request:
    ///
    ///     POST /Todo
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
    /// <response code="200">Возвращает вектор решения, точность решения и количество итераций</response>
    /// <response code="500">На сервере что-то пошло не так</response>
    /// <returns>Решение уравнения</returns>
    [HttpPost(Name = "vector-fem")]
    [ProducesResponseType(typeof(FemResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateCalculation([FromBody] TestSession testSessionParameters)
    {
        Console.WriteLine($"[Started session] Id: {testSessionParameters.Id}");

        try
        {
            _logger.LogInformation($"[{nameof(FemController)}] {nameof(CreateCalculation)} initialize");

            _logger.LogInformation($"[{nameof(FemController)}] create test session");
            var testSession = await _testSessionService.CreateTestSessionAsync(testSessionParameters);

            _logger.LogInformation($"[{nameof(FemController)}] resolve matrix portrait");
            var matrixProfile =
                await _portraitService.ResolveMatrixPortraitAsync(testSession.Mesh, EMatrixFormats.Profile);
            await matrixProfile.InitializeVectorsAsync(
                testSession.Mesh.Elements.SelectMany(element => element.Edges).DistinctBy(edge => edge.EdgeIndex)
                    .Count()
            );

            _logger.LogInformation($"[{nameof(FemController)}] calculate global matrix");
            await _globalMatrixServices.GetGlobalMatrixAsync(matrixProfile, testSession);

            _logger.LogInformation($"[{nameof(FemController)}] calculate right part vector");
            await _rightPartVectorService.GetRightPartVectorAsync(matrixProfile, testSession);

            _logger.LogInformation($"[{nameof(FemController)}] resolve boundary conditions");
            var boundaryConditionService =
                await _boundaryCondition.ResolveBoundaryConditionAsync(testSession.BoundaryCondition);

            _logger.LogInformation($"[{nameof(FemController)}] set boundary conditions");
            await boundaryConditionService.SetBoundaryConditionsAsync(testSession, matrixProfile);

            _logger.LogInformation($"[{nameof(FemController)}] calculate slae");
            var solutionVector = await _solverService.GetSolutionVectorAsync(matrixProfile, 1000, 1e-15);

#if true
            await _visualizerService.WriteMatrixToFileAsync(matrixProfile);
            await _visualizerService.DrawMeshPlotAsync(testSession.Mesh);
#endif

            var result = new FemResponse
            {
                SolutionVector = solutionVector.solve.Data,
                Accuracy = solutionVector.discrepancy,
                IterationsCount = solutionVector.iterCount
            };

            return Ok(result);
        }
        catch (Exception exception)
        {
            return BadRequest($"{StatusCodes.Status500InternalServerError.ToString()}, {exception}");
        }
        finally
        {
            Console.WriteLine($"[Ended session] Id: {testSessionParameters.Id}");
        }
    }
}
using FEM.Common.Enums;
using FEM.Server.Data.Domain;
using FEM.Server.Services.Parallelepipedal.BoundaryConditionService;
using FEM.Server.Services.Parallelepipedal.DrawingMeshService;
using FEM.Server.Services.Parallelepipedal.GlobalMatrixService;
using FEM.Server.Services.Parallelepipedal.MatrixPortraitService;
using FEM.Server.Services.Parallelepipedal.RightPartVectorService;
using FEM.Server.Services.TestSessionService;
using Microsoft.AspNetCore.Mvc;

namespace FEM.Server.Controllers;

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

    public FemController(
        ILogger<FemController> logger,
        IGlobalMatrixServices globalMatrixServices,
        ITestSessionService testSessionService,
        IMatrixPortraitService portraitService,
        IRightPartVectorService rightPartVectorService,
        IVisualizerService visualizerService,
        IBoundaryConditionFactory boundaryCondition
    )
    {
        _logger = logger;
        _globalMatrixServices = globalMatrixServices;
        _testSessionService = testSessionService;
        _portraitService = portraitService;
        _rightPartVectorService = rightPartVectorService;
        _visualizerService = visualizerService;
        _boundaryCondition = boundaryCondition;
    }

    [HttpPost(Name = "vector-fem")]
    public async Task<IActionResult> CreateCalculation([FromBody] TestSession testSessionParameters)
    {
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

            _logger.LogInformation($"[{nameof(FemController)}] resolve slae solver");
            _logger.LogInformation($"[{nameof(FemController)}] ...");

            _logger.LogInformation($"[{nameof(FemController)}] calculate slae");
            _logger.LogInformation($"[{nameof(FemController)}] ...");
#if DEBUG
            await _visualizerService.WriteMatrixToFileAsync(matrixProfile);
            await _visualizerService.DrawMeshPlotAsync(testSession.Mesh);
#endif
            var result = "The end";
            return Ok(result);
        }
        catch (Exception exception)
        {
            return BadRequest($"{StatusCodes.Status500InternalServerError.ToString()}, {exception}");
        }
    }
}
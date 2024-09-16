using FEM.Common.Enums;
using FEM.Core.Services.Parallelepipedal.BoundaryConditionService;
using FEM.Core.Services.Parallelepipedal.DrawingMeshService;
using FEM.Core.Services.Parallelepipedal.GlobalMatrixService;
using FEM.Core.Services.Parallelepipedal.MatrixPortraitService;
using FEM.Core.Services.Parallelepipedal.RightPartVectorService;
using FEM.Core.Services.TestSessionService;

namespace FEM.Core;

public class Startup
{
    private readonly IGlobalMatrixServices _globalMatrixServices;
    private readonly ITestSessionService _testSessionService;
    private readonly IMatrixPortraitService _portraitService;
    private readonly IRightPartVectorService _rightPartVectorService;
    private readonly IVisualizerService _visualizerService;
    private readonly IBoundaryConditionFactory _boundaryCondition;

    public Startup(
        IGlobalMatrixServices globalMatrixServices,
        ITestSessionService testSessionService,
        IMatrixPortraitService portraitService,
        IRightPartVectorService rightPartVectorService,
        IVisualizerService visualizerService,
        IBoundaryConditionFactory boundaryCondition
    )
    {
        _globalMatrixServices = globalMatrixServices;
        _testSessionService = testSessionService;
        _portraitService = portraitService;
        _rightPartVectorService = rightPartVectorService;
        _visualizerService = visualizerService;
        _boundaryCondition = boundaryCondition;
    }

    public async Task Run()
    {
        var testSession = await _testSessionService.CreateTestSessionAsync();

        var matrixProfile = await _portraitService.ResolveMatrixPortraitAsync(testSession.Mesh, EMatrixFormats.Profile);
        await matrixProfile.InitializeVectorsAsync(
            testSession.Mesh.Elements.SelectMany(element => element.Edges).DistinctBy(edge => edge.EdgeIndex).Count()
        );

        await _globalMatrixServices.GetGlobalMatrixAsync(matrixProfile, testSession);
        await _rightPartVectorService.GetRightPartVectorAsync(matrixProfile, testSession);

        var boundaryConditionService =
            await _boundaryCondition.ResolveBoundaryConditionAsync(testSession.BoundaryCondition);
        await boundaryConditionService.SetBoundaryConditionsAsync(testSession, matrixProfile);


#if DEBUG
        await _visualizerService.DrawMeshPlotAsync(testSession.Mesh);
        await _visualizerService.WriteMatrixToFileAsync(matrixProfile);
#endif

        Console.ReadKey();
    }
}
using FEM.Common.Enums;
using FEM.Common.Resolvers.MatrixFormatResolver;
using VectorFEM.Core.Services.Parallelepipedal.DrawingMeshService;
using VectorFEM.Core.Services.Parallelepipedal.GlobalMatrixService;
using VectorFEM.Core.Services.Parallelepipedal.MatrixPortraitService;
using VectorFEM.Core.Services.Parallelepipedal.MeshService;
using VectorFEM.Core.Services.Parallelepipedal.RightPartVectorService;
using VectorFEM.Core.Services.TestSessionService;

namespace FEM.Core;

public class Startup
{
    private readonly IGlobalMatrixServices   _globalMatrixServices;
    private readonly ITestSessionService     _testSessionService;
    private readonly IMatrixPortraitService  _portraitService;
    private readonly IRightPartVectorService _rightPartVectorService;
    private readonly IVisualizerService      _visualizerService;

    public Startup(
        IGlobalMatrixServices globalMatrixServices,
        ITestSessionService testSessionService,
        IMatrixPortraitService portraitService,
        IRightPartVectorService rightPartVectorService,
        IVisualizerService visualizerService
    )
    {
        _globalMatrixServices = globalMatrixServices;
        _testSessionService = testSessionService;
        _portraitService = portraitService;
        _rightPartVectorService = rightPartVectorService;
        _visualizerService = visualizerService;
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

#if DEBUG
        await _visualizerService.DrawMeshPlotAsync(testSession.Mesh);
        await _visualizerService.WriteMatrixToFileAsync(matrixProfile);
#endif

        // Console.ReadKey();
    }
}
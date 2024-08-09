using FEM.Common.Data.TestSession;
using VectorFEM.Core.Data.Parallelepipedal;
using VectorFEM.Core.Services.Parallelepipedal.MeshService;

namespace VectorFEM.Core.Services.TestSessionService;

public class TestSessionService : ITestSessionService
{
    private readonly IMeshService _meshService;

    public TestSessionService(IMeshService meshService)
    {
        _meshService = meshService;
    }

    public async Task<TestSession<Mesh>> CreateTestSessionAsync()
    {
        var testConfiguration = await _meshService.GenerateTestConfiguration();
        var mesh = await _meshService.GenerateMeshAsync();

        return await Task.FromResult(
            new TestSession<Mesh>
            {
                Mesh = mesh,
                Mu = testConfiguration.AdditionalParameters.Mu,
                Gamma = testConfiguration.AdditionalParameters.Gamma
            }
        );
    }
}
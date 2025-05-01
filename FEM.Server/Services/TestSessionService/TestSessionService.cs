using FEM.Common.Data.TestSession;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Services.Parallelepipedal.MeshService;
using FEM.Server.Services.PlotService;

namespace FEM.Server.Services.TestSessionService;

/// <inheritdoc />
public class TestSessionService : ITestSessionService
{
    private readonly IMeshService _meshService;

    public TestSessionService(IMeshService meshService)
    {
        _meshService = meshService;
    }

    /// <inheritdoc />
    public async Task<TestSession<Mesh>> CreateTestSessionAsync(TestSession testSession)
    {
        var testConfiguration = await _meshService.GenerateTestConfiguration(testSession);
        var mesh = await _meshService.GenerateMeshAsync(testConfiguration);

        await _meshService.AssignMuesAsync(mesh, testConfiguration);

        return await Task.FromResult(
            new TestSession<Mesh>
            {
                Mesh = mesh,
                Mu = testConfiguration.AdditionalParameters.Mu,
                Gamma = testConfiguration.AdditionalParameters.Gamma,
                BoundaryCondition = testConfiguration.AdditionalParameters.BoundaryCondition
            }
        );
    }
}
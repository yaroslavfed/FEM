using FEM.Common.Data.TestSession;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Services.Parallelepipedal.MeshService;
using FEM.Server.Services.PlotService;

namespace FEM.Server.Services.TestSessionService;

public class TestSessionService : ITestSessionService
{
    private readonly IMeshService _meshService;
    private readonly IPlotService _plotService;

    public TestSessionService(IMeshService meshService, IPlotService plotService)
    {
        _meshService = meshService;
        _plotService = plotService;
    }

    public async Task<TestSession<Mesh>> CreateTestSessionAsync()
    {
        var testConfiguration = await _meshService.GenerateTestConfiguration();
        var mesh = await _meshService.GenerateMeshAsync(testConfiguration);


        return await Task.FromResult(
            new TestSession<Mesh>
            {
                Mesh = mesh,
                Mu = testConfiguration.AdditionalParameters.Mu,
                Gamma = testConfiguration.AdditionalParameters.Gamma,
                BoundaryCondition = testConfiguration.AdditionalParameters.BoundaryCondition - 1
            }
        );
    }

    public async Task<TestSession<Mesh>> CreateTestSessionAsync(TestSession testSession)
    {
        var testConfiguration = await _meshService.GenerateTestConfiguration(testSession);
        var mesh = await _meshService.GenerateMeshAsync(testConfiguration);
        await _plotService.ShowPlotAsync(mesh);

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
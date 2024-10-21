using FEM.Common.Data.MeshModels;
using FEM.NonStationary.DTO.TestingContext;
using FEM.Server.Data.InputModels;
using FEM.Server.Services.Parallelepipedal.MeshService;

namespace FEM.Server.Services.TestSessionService;

public class TestSessionService : ITestSessionService
{
    private readonly IMeshService _meshService;

    public TestSessionService(IMeshService meshService)
    {
        _meshService = meshService;
    }

    public async Task<NonStationaryTestSession<Mesh>> CreateTestSessionAsync()
    {
        var testConfiguration = await _meshService.GenerateTestConfiguration();
        var mesh = await _meshService.GenerateMeshAsync(testConfiguration);

        return await Task.FromResult(
            new NonStationaryTestSession<Mesh>
            {
                Mesh = mesh,
                Mu = testConfiguration.AdditionalParameters.Mu,
                Gamma = testConfiguration.AdditionalParameters.Gamma,
                BoundaryCondition = testConfiguration.AdditionalParameters.BoundaryCondition - 1
            }
        );
    }

    public async Task<NonStationaryTestSession<Mesh>> CreateTestSessionAsync(TestSession testSession)
    {
        var testConfiguration = await _meshService.GenerateTestConfiguration(testSession);
        var mesh = await _meshService.GenerateMeshAsync(testConfiguration);

        return await Task.FromResult(
            new NonStationaryTestSession<Mesh>
            {
                Mesh = mesh,
                Mu = testConfiguration.AdditionalParameters.Mu,
                Gamma = testConfiguration.AdditionalParameters.Gamma,
                BoundaryCondition = testConfiguration.AdditionalParameters.BoundaryCondition
            }
        );
    }
}
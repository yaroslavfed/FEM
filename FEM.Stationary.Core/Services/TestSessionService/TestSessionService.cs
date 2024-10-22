using FEM.Common.Core.Services.TestSessionService;
using FEM.Common.DTO.Abstractions;
using FEM.Common.Services.MeshService;
using FEM.Stationary.DTO.Configurations;
using FEM.Stationary.DTO.TestingContext;

namespace FEM.Stationary.Core.Services.TestSessionService;

public class TestSessionService : ITestSessionService
{
    private readonly IMeshService _meshService;

    public TestSessionService(IMeshService meshService)
    {
        _meshService = meshService;
    }

    /// <summary>
    /// Создаем сессию тестирования<br />
    /// Генерируем конфигурацию и строим сетку
    /// </summary>
    /// <param name="testConfiguration"><see cref="StationaryTestConfiguration">Параметры сессии тестирования</see></param>
    /// <returns></returns>
    public async Task<TestSessionBase> CreateTestSessionAsync(StationaryTestConfiguration testConfiguration)
    {
        var testSessionParameters = await _meshService.GenerateTestConfiguration(testConfiguration);
        var mesh = await _meshService.GenerateMeshAsync(testSessionParameters);

        return await Task.FromResult(
            new StationaryTestSession
            {
                Mesh = mesh,
                Mu = testConfiguration.AdditionParameters.MuCoefficient,
                Gamma = testConfiguration.AdditionParameters.GammaCoefficient,
                BoundaryCondition = testSessionParameters.BoundaryCondition
            }
        );
    }
}
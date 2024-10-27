using FEM.Core.Services.MeshService;
using FEM.SharedDTO.Abstractions;
using Stationary.DTO.Configurations;
using Stationary.DTO.TestingContext;

namespace Stationary.Core.Services.TestSessionService;

public class StationaryTestSessionService : IStationaryTestSessionService
{
    private readonly IMeshService _meshService;

    public StationaryTestSessionService(IMeshService meshService)
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
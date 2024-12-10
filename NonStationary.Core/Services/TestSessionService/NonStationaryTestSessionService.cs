using FEM.SharedCore.Services.MeshService;
using FEM.SharedCore.Services.TestSessionService;
using NonStationary.Core.Services.TimeService;
using NonStationary.DTO.Configurations;
using NonStationary.DTO.TestingContext;

namespace NonStationary.Core.Services.TestSessionService;

public class NonStationaryTestSessionService : ITestSessionService<NonStationaryTestSession, NonStationaryTestConfiguration>
{
    private readonly IMeshService _meshService;
    private readonly ITimeService _timeService;

    public NonStationaryTestSessionService(IMeshService meshService, ITimeService timeService)
    {
        _meshService = meshService;
        _timeService = timeService;
    }

    /// <summary>
    /// Создаем сессию тестирования<br />
    /// Генерируем конфигурацию и строим сетку
    /// </summary>
    /// <param name="testConfiguration"><see cref="StationaryTestConfiguration">Параметры сессии тестирования</see></param>
    /// <returns></returns>
    public async Task<NonStationaryTestSession> CreateTestSessionAsync(NonStationaryTestConfiguration testConfiguration)
    {
        var testSessionParameters = await _meshService.GenerateTestConfiguration(testConfiguration);
        var meshGrid = await _meshService.GenerateMeshAsync(testSessionParameters);
        var timeGrid = await _timeService.GetTimeGridParametersAsync(testConfiguration.TimeGridParameters);
        
        return await Task.FromResult(
            new NonStationaryTestSession
            {
                Mesh = meshGrid,
                BoundaryCondition = testSessionParameters.BoundaryCondition,
                // TODO: Доделть создание моделей катушек и сетки по времени
                CoilParameters = null,
                TimeGrid = new() { GridList = timeGrid }
            }
        );
    }
}
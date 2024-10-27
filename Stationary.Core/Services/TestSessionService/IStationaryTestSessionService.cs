using FEM.SharedDTO.Abstractions;
using Stationary.DTO.Configurations;

namespace Stationary.Core.Services.TestSessionService;

/// <summary>
/// Сервис создания сессии тестирования
/// </summary>
public interface IStationaryTestSessionService
{
    /// <summary>
    /// Создаем сессию тестирования
    /// </summary>
    /// <remarks>Для использования с внешними в API</remarks>
    /// <returns>Сессия тестирования расчётной области</returns>
    Task<TestSessionBase> CreateTestSessionAsync(StationaryTestConfiguration testConfiguration);
}
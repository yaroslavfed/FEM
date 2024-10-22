using FEM.Common.DTO.Abstractions;
using FEM.Stationary.DTO.Configurations;

namespace FEM.Common.Core.Services.TestSessionService;

/// <summary>
/// Сервис создания сессии тестирования
/// </summary>
public interface ITestSessionService
{
    /// <summary>
    /// Создаем сессию тестирования
    /// </summary>
    /// <remarks>Для использования с внешними в API</remarks>
    /// <returns>Сессия тестирования расчётной области</returns>
    Task<TestSessionBase> CreateTestSessionAsync(StationaryTestConfiguration testConfiguration);
}
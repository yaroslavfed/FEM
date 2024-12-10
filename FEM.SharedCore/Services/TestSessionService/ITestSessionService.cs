using FEM.SharedDTO.Abstractions;
using FEM.SharedDTO.Configurations.TestConfiguration;

namespace FEM.SharedCore.Services.TestSessionService;

/// <summary>
/// Сервис создания сессии тестирования
/// </summary>
public interface ITestSessionService<TSessionData, in TConfigurationData> 
    where TSessionData : TestSessionBase
    where TConfigurationData : TestConfigurationBase
{
    /// <summary>
    /// Создаем сессию тестирования
    /// </summary>
    /// <remarks>Для использования с внешними в API</remarks>
    /// <returns>Сессия тестирования расчётной области</returns>
    Task<TSessionData> CreateTestSessionAsync(TConfigurationData testConfiguration);
}
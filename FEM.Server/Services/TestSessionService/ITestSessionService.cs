using FEM.Common.Data.TestSession;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.TestSessionService;

/// <summary>
/// Сервис создания сессии тестирования
/// </summary>
public interface ITestSessionService
{
    /// <summary>
    /// Создаем сессию тестирования
    /// </summary>
    /// <remarks>Для использования с внутренними данными получаемыми из конфига</remarks>
    /// <returns>Сессия тестирования расчётной области</returns>
    Task<TestSession<Mesh>> CreateTestSessionAsync();

    /// <summary>
    /// Создаем сессию тестирования
    /// </summary>
    /// <remarks>Для использования с внешними в API</remarks>
    /// <returns>Сессия тестирования расчётной области</returns>
    Task<TestSession<Mesh>> CreateTestSessionAsync(TestSession testSession);
}
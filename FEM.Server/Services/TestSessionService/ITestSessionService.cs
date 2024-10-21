using FEM.Common.Data.MeshModels;
using FEM.NonStationary.DTO.TestingContext;
using FEM.Server.Data.InputModels;

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
    Task<NonStationaryTestSession<Mesh>> CreateTestSessionAsync();

    /// <summary>
    /// Создаем сессию тестирования
    /// </summary>
    /// <remarks>Для использования с внешними в API</remarks>
    /// <returns>Сессия тестирования расчётной области</returns>
    Task<NonStationaryTestSession<Mesh>> CreateTestSessionAsync(TestSession testSession);
}
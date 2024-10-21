using FEM.Common.Data.TestSession;
using FEM.Server.Data.InputModels;

namespace FEM.Server.Extensions;

/// <summary>
/// Расширения для сессии тестирования
/// </summary>
public static class TestSessionExtensions
{
    /// <summary>
    /// Генерируем сессию тестирования из полученных данных
    /// </summary>
    /// <param name="base"><see cref="TestSessionBase">Базовый класс сессии тестирования</see></param>
    /// <returns><see cref="TestSession">Серверную модель сессии тестирования</see></returns>
    public static TestSession InitializeTestSession(this TestSessionBase @base) =>
        new() { Id = Guid.NewGuid(), TestSessionParameters = @base };
}
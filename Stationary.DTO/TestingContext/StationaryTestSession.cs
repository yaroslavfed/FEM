using FEM.SharedDTO.Abstractions;

namespace Stationary.DTO.TestingContext;

/// <summary>
/// Параметры сессии тестирования стационарной задачи
/// </summary>
public record StationaryTestSession : TestSessionBase
{
    /// <summary>
    /// Коэффициент мю
    /// </summary>
    public double Mu { get; init; }

    /// <summary>
    /// Коэффициент гамма
    /// </summary>
    public double Gamma { get; init; }
}
using FEM.Common.Data.TestSession;
using FEM.NonStationary.DTO.InputModels;

namespace FEM.NonStationary.DTO.TestingContext;

/// <summary>
/// Параметры сессии тестирования нестационарной задачи
/// </summary>
public record NonStationaryTestSession : TestSessionBase
{
    /// <summary>
    /// Параметры сетки катушек
    /// </summary>
    public required CoilParameters CoilParameters { get; init; }

    /// <summary>
    /// Параметры сетки по времени
    /// </summary>
    public required TimeGridParameters TimeGridParameters { get; init; }
}
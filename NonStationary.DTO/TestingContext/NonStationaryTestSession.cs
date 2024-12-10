using FEM.SharedDTO.Abstractions;
using NonStationary.DTO.GridModels;
using NonStationary.DTO.InputModels;

namespace NonStationary.DTO.TestingContext;

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
    public required TimeGrid TimeGrid { get; init; }
}
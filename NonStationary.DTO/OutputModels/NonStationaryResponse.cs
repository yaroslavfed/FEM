namespace NonStationary.DTO.OutputModels;

/// <summary>
/// Модель результата решения задачи
/// </summary>
public class NonStationaryResponse
{
    /// <summary>
    /// Идентификатор полученного результата
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Относительная погрешность решения
    /// </summary>
    public required double Discrepancy { get; init; }

    /// <summary>
    /// Количество итераций
    /// </summary>
    public required int IterationsCount { get; init; }
}
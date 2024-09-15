namespace FEM.Server.Data;

/// <summary>
/// Модель результата решения задачи
/// </summary>
public class FemResponse
{
    /// <summary>
    /// Идентификатор полученного результата
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Невязка решения
    /// </summary>
    public required double Discrepancy { get; init; }

    /// <summary>
    /// Количество итераций
    /// </summary>
    public required int IterationsCount { get; init; }
}
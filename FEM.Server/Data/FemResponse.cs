namespace FEM.Server.Data;

/// <summary>
/// Модель решения задачи
/// </summary>
public class FemResponse
{
    /// <summary>
    /// Вектор q
    /// </summary>
    public required IReadOnlyList<double> SolutionVector { get; set; }

    /// <summary>
    /// Точность решения
    /// </summary>
    public required double Accuracy { get; set; }

    /// <summary>
    /// Количество итераций
    /// </summary>
    public required int IterationsCount { get; set; }
}
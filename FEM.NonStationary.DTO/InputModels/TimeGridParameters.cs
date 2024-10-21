namespace FEM.NonStationary.DTO.InputModels;

/// <summary>
/// Параметры сетки по времени
/// </summary>
public record TimeGridParameters
{
    /// <summary>
    /// Начало и конец сетки по времени
    /// </summary>
    public double[] TimeGridBounds { get; init; } = [];

    /// <summary>
    /// Шаг у сетки по времени
    /// /// </summary>
    public double TimeGridSplittingCoefficient { get; init; }

    /// <summary>
    /// Коэффициент разрядки у сетки по времени
    /// /// </summary>
    public double TimeGridMultiplyCoefficient { get; init; }

    /// <summary>
    /// Вложенность сетки по времени
    /// /// </summary>
    public int TimeGridNested { get; init; }

    /// <summary>
    /// Точка до которой будет линейно убывать толк
    /// /// </summary>
    public double TimeDecreasingPoint { get; init; }

    /// <summary>
    /// Количество разбиений между нулем и минимумом
    /// /// </summary>
    public int TimeDecreasingSplitting { get; init; }
}
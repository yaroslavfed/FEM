using FEM.Common.Data.MathModels;

namespace FEM.NonStationary.DTO.InputModels;

/// <summary>
/// Параметры катушек
/// </summary>
public record CoilParameters
{
    /// <summary>
    /// Координаты катушки
    /// </summary>
    public required Point3D Coordinate { get; init; }

    /// <summary>
    /// Количество разбиений внутри катушки по x, y
    /// <br />
    /// Z - количество разбиений между катушкой и средой
    /// </summary>
    public int[] SplittingCoefficient { get; init; } = [];

    /// <summary>
    /// Коэффициент разрядки по осям от катушки
    /// </summary>
    public double[] MultiplyCoefficient { get; init; } = [];

    /// <summary>
    /// Количество разбиений каждого ребра катушки
    /// </summary>
    public int EdgesSplittingCoefficient { get; init; }

    /// <summary>
    /// Направление тока на катушке
    /// </summary>
    public int CurrentDirection { get; init; }
}
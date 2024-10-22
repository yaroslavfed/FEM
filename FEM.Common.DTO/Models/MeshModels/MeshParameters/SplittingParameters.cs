namespace FEM.Common.DTO.Models.MeshModels.MeshParameters;

/// <summary>
/// Параметры разбиения расчётной области
/// </summary>
public record SplittingParameters
{
    /// <summary>
    /// Количество разбиений по OX
    /// </summary>
    public double XSplittingCoefficient { get; init; }

    /// <summary>
    /// Количество разбиений по OY
    /// </summary>
    public double YSplittingCoefficient { get; init; }

    /// <summary>
    /// Количество разбиений по OZ
    /// </summary>
    public double ZSplittingCoefficient { get; init; }

    /// <summary>
    /// Коэффициент разрядки по OX
    /// </summary>
    public double XMultiplyCoefficient { get; init; }

    /// <summary>
    /// Коэффициент разрядки по OY
    /// </summary>
    public double YMultiplyCoefficient { get; init; }

    /// <summary>
    /// Коэффициент разрядки по OZ
    /// </summary>
    public double ZMultiplyCoefficient { get; init; }
}
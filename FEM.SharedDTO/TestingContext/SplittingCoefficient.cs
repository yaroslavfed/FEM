using FEM.SharedDTO.Models.MathModels;

namespace FEM.SharedDTO.TestingContext;

/// <summary>
/// Коэффициент дробления сетки
/// </summary>
public record SplittingCoefficient
{
    /// <summary>
    /// Коэффициент дробления сетки по трем осям
    /// </summary>
    public required Point3D Coefficient { get; init; }
}
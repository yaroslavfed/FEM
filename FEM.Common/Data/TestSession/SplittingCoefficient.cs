using FEM.Common.Data.MathModels;

namespace FEM.Shared.Domain.Data.TestSession;

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
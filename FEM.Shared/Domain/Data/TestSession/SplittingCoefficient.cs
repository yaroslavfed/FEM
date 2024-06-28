using FEM.Shared.Domain.MathModels;

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
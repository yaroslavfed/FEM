using FEM.Common.Data.MathModels;

namespace FEM.Common.Data.TestingContext;

/// <summary>
/// Кожффициент разрядки
/// </summary>
public record MultiplyCoefficient
{
    /// <summary>
    /// Коэффициент разрядки по трем осям
    /// </summary>
    public required Point3D Coefficient { get; init; }
}
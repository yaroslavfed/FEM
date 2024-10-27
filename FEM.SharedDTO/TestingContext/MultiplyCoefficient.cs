using FEM.SharedDTO.Models.MathModels;

namespace FEM.SharedDTO.TestingContext;

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
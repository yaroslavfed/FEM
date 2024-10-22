using FEM.Common.DTO.Models.MathModels;

namespace FEM.Common.DTO.TestingContext;

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
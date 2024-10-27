using FEM.SharedDTO.Models.MathModels;

namespace FEM.SharedDTO.Models.InputModels;

/// <summary>
/// Набор параметров позиционирования объекта
/// </summary>
public record Positioning
{
    /// <summary>
    /// Координата центра объекта в декартовой системе координат
    /// </summary>
    public required Point3D Coordinate { get; init; }

    /// <summary>
    /// Расстояние от центра объекта до его границ
    /// </summary>
    public required Point3D BoundsDistance { get; init; }
}
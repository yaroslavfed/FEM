using FEM.Common.Data.MathModels;

namespace FEM.Common.Data.InputModels;

public record Positioning
{
    public required Point3D Coordinate { get; init; }

    public required Point3D BoundsDistance { get; init; }
}
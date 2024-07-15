using FEM.Shared.Domain.MathModels;

namespace FEM.Common.Data.InputModels;

public abstract record Positioning
{
    public required Point3D Coordinate { get; init; }

    public required Point3D BoundsDistance { get; init; }
}
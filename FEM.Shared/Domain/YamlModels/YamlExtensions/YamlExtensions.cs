using FEM.Shared.Domain.MathModels;

namespace FEM.Shared.Domain.YamlModels.YamlExtensions;

public static class YamlExtensions
{
    public static Point3D GetLowPoint3D(this Positioning positioning)
    {
        return new()
        {
            X = positioning.Coordinate.X - positioning.BoundsDistance.X,
            Y = positioning.Coordinate.Y - positioning.BoundsDistance.Y,
            Z = positioning.Coordinate.Z - positioning.BoundsDistance.Z
        };
    }
    
    public static Point3D GetHighPoint3D(this Positioning positioning)
    {
        return new()
        {
            X = positioning.Coordinate.X + positioning.BoundsDistance.X,
            Y = positioning.Coordinate.Y + positioning.BoundsDistance.Y,
            Z = positioning.Coordinate.Z + positioning.BoundsDistance.Z
        };
    }
}
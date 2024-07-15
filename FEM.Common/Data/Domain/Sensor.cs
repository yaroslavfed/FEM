using FEM.Common.Data.MathModels;

namespace FEM.Common.Data.Domain;

public record Sensor
{
    public required Point3D Coordinate { get; init; }
}
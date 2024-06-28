using FEM.Shared.Domain.MathModels;

namespace FEM.Shared.Domain.Data;

public record Sensor
{
    public required Point3D Coordinate { get; init; }
}
using FEM.Shared.Domain.MathModels;

namespace FEM.Shared.Domain;

public record Sensor
{
    public required Point3D Coordinate { get; init; }
}
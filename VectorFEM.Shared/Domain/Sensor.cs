using VectorFEM.Shared.Domain.MathModels;

namespace VectorFEM.Shared.Domain;

public record Sensor
{
    public required Point3D Coordinate { get; init; }
}
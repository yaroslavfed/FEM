using FEM.SharedDTO.Models.MathModels;

namespace FEM.SharedDTO.Domain;

public record Sensor
{
    public required Point3D Coordinate { get; init; }
}
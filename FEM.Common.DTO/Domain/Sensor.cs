using FEM.Common.DTO.Models.MathModels;

namespace FEM.Common.DTO.Domain;

public record Sensor
{
    public required Point3D Coordinate { get; init; }
}
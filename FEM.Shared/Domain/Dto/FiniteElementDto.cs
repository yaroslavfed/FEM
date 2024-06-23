using FEM.Shared.Domain.MathModels;

namespace FEM.Shared.Domain.Dto;

public record FiniteElementDto
{
    public required Point3D LowCoordinate { get; init; }
    
    public required Point3D HighCoordinate { get; init; }
}
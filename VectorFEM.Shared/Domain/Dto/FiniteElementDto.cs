using VectorFEM.Shared.Domain.MathModels;

namespace VectorFEM.Shared.Domain.Dto;

public record FiniteElementDto
{
    public Point3D LowCoordinate { get; init; }
    
    public Point3D HighCoordinate { get; init; }
}
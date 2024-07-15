namespace FEM.Common.Data.Domain;

public record Mesh
{
    public IEnumerable<FiniteElement> Elements { get; init; } = [];
}
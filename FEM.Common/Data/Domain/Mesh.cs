namespace FEM.Shared.Domain.Data;

public record Mesh
{
    public IEnumerable<FiniteElement> Elements { get; init; } = [];
}
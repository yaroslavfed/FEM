namespace FEM.Shared.Domain.Data;

public record FiniteElement
{
    public IEnumerable<Edge> Edges { get; init; } = [];
}
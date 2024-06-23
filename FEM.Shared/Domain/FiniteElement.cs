namespace FEM.Shared.Domain;

public record FiniteElement
{
    public IEnumerable<Edge> Edges { get; init; } = [];
}
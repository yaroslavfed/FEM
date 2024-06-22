namespace VectorFEM.Shared.Domain;

public record FiniteElement
{
    public IEnumerable<Edge> Edges { get; init; } = [];
}
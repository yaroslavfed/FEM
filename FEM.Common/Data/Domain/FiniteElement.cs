namespace FEM.Common.Data.Domain;

public record FiniteElement
{
    public IEnumerable<Edge> Edges { get; init; } = [];
}
namespace FEM.Common.Data.Domain;

public record FiniteElement
{
    public IList<Edge> Edges { get; init; } = [];
}
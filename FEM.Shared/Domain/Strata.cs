namespace FEM.Shared.Domain;

public record Strata
{
    public IEnumerable<FiniteElement> Elements { get; init; } = [];
}
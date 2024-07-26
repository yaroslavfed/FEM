namespace FEM.Core.Data;

/// <summary>
/// Структура конечного элемента с нумерованными узлами и ребрами
/// </summary>
public record FiniteElementWithNumerics
{
    public IList<int> Nodes { get; set; } = [];

    public IList<int> Edges { get; set; } = [];

    public IList<(int First, int Second)> MapNodesEdges { get; set; } = [];
}
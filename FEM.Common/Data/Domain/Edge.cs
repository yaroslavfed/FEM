namespace FEM.Common.Data.Domain;

/// <summary>
/// Структура ребра конечного элемента
/// </summary>
public record Edge
{
    public int EdgeNumber { get; init; }
    public IEnumerable<Node> Nodes { get; init; } = new Node[2];
}
using VectorFEM.Shared.Domain;

namespace FEM.Shared.Domain;

/// <summary>
/// Структура ребра конечного элемента
/// </summary>
/// <param name="EdgeNumber">Номер ребра в конечном элементе</param>
/// <param name="FirstNode">Первый узел ребра</param>
/// <param name="SecondNode">Второй узел ребра</param>
public record Edge
{
    public int EdgeNumber { get; init; }
    public IEnumerable<Node> Nodes { get; init; } = new Node[2];
}
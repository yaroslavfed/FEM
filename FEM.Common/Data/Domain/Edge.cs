namespace FEM.Common.Data.Domain;

/// <summary>
/// Структура ребра конечного элемента
/// </summary>
public record Edge
{
    /// <summary>
    /// Номер ребра в конечном элементе
    /// </summary>
    public int EdgeIndex { get; init; }

    /// <summary>
    /// Узлы принадлежащие ребру
    /// </summary>
    public IList<Node> Nodes { get; init; } = [];
}
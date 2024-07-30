using FEM.Common.Data.Domain;

namespace VectorFEM.Core.Data.Parallelepipedal;

/// <summary>
/// Параллелепипедальный конечный элемент векторного МКЭ
/// </summary>
public record FiniteElement
{
    /// <summary>
    /// Список ребер принадлежащих КЭ
    /// </summary>
    public IList<Edge> Edges { get; init; } = new List<Edge>();
}
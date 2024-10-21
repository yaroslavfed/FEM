using FEM.Common.Data.Domain;

namespace FEM.Common.Data.MeshModels;

/// <summary>
/// Параллелепипедальный КЭ векторного МКЭ
/// </summary>
public record FiniteElement
{
    /// <summary>
    /// Список ребер принадлежащих КЭ
    /// </summary>
    public IList<Edge> Edges { get; init; } = new List<Edge>();
}
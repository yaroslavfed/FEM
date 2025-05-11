using FEM.Common.Data.Domain;

namespace FEM.Server.Data.Parallelepipedal;

/// <summary>
/// Параллелепипедальная сетка исследуемого объекта
/// </summary>
public record Mesh
{
    /// <summary>
    /// Список конечных элементов расчётной области
    /// </summary>
    public List<FiniteElement> Elements { get; init; } = [];

    /// <summary>
    /// Получаем ребро по его номеру
    /// </summary>
    /// <param name="index">Глобальный индекс ребра</param>
    /// <returns>Объект ребра</returns>
    public Edge GetEdgeByIndex(int index) =>
        (from element in Elements from edge in element.Edges where edge.EdgeIndex == index select edge).Single();
}
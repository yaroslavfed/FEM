namespace FEM.Common.Data.Domain;

/// <summary>
/// Сетка исследуемого объекта
/// </summary>
public record Mesh
{
    /// <summary>
    /// Список конечных элементов в сетке
    /// </summary>
    public IEnumerable<FiniteElement> Elements { get; init; } = [];
}
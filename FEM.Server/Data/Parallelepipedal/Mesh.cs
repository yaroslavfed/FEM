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
}
using FEM.Common.Enums;

namespace FEM.Common.Data.TestSession;

/// <summary>
/// Параметры сессии решения прямой задачи
/// </summary>
public record TestSession<TMesh>
{
    /// <summary>
    /// Расчётная область
    /// </summary>
    public required TMesh Mesh { get; init; }

    public int Mu { get; init; }

    public int Gamma { get; init; }
    
    /// <summary>
    /// Краевое условие
    /// </summary>
    public EBoundaryConditions BoundaryCondition { get; init; }
}
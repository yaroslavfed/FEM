using FEM.Common.Data.MeshModels;
using FEM.Common.Enums;

namespace FEM.Common.Data.TestSession;

/// <summary>
/// Базовый класс параметров сессии решения прямой задачи
/// </summary>
public abstract record TestSessionBase
{
    /// <summary>
    /// Расчётная область
    /// </summary>
    public required Mesh Mesh { get; init; }

    /// <summary>
    /// Краевое условие
    /// </summary>
    public EBoundaryConditions BoundaryCondition { get; init; }
}
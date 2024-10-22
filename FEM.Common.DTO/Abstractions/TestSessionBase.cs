using FEM.Common.DTO.Enums;
using FEM.Common.DTO.Models.MeshModels;

namespace FEM.Common.DTO.Abstractions;

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
using FEM.Common.DTO.Enums;
using FEM.Common.DTO.Models.MeshModels.MeshParameters;
using FEM.NonStationary.DTO.InputModels;

namespace FEM.NonStationary.DTO.TestingContext;

/// <summary>
/// Параметры сессии решения нестационарного уравнения
/// </summary>
public record NonStationaryComputationalDomain
{
    /// <summary>
    /// Параметры расчётной области
    /// </summary>
    public required MeshParameters MeshParameters { get; init; }

    /// <summary>
    /// Параметры сетки катушек
    /// </summary>
    public required CoilParameters CoilParameters { get; init; }

    /// <summary>
    /// Параметры сетки по времени
    /// </summary>
    public required TimeGridParameters TimeGridParameters { get; init; }

    /// <summary>
    /// Краевое условие
    /// </summary>
    public EBoundaryConditions BoundaryCondition { get; init; }
}
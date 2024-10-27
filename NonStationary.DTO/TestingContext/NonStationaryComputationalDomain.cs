using FEM.SharedDTO.Enums;
using FEM.SharedDTO.Models.MeshModels.MeshParameters;
using NonStationary.DTO.InputModels;

namespace NonStationary.DTO.TestingContext;

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
using FEM.Common.DTO.Enums;
using FEM.Common.DTO.Models.MeshModels.MeshParameters;

namespace FEM.Stationary.DTO.TestingContext;

/// <summary>
/// Параметры сессии решения стационарного уравнения
/// </summary>
public record StationaryComputationalDomain
{
    /// <summary>
    /// Параметры расчётной области
    /// </summary>
    public required MeshParameters MeshParameters { get; init; }

    public double Mu { get; init; }

    public double Gamma { get; init; }

    /// <summary>
    /// Краевое условие
    /// </summary>
    public EBoundaryConditions BoundaryCondition { get; init; }
}
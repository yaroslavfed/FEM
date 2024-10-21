using FEM.Common.Data.MeshModels.MeshParameters;
using FEM.Common.Enums;

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
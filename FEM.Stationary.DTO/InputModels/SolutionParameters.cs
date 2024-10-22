using FEM.Common.DTO.Enums;

namespace FEM.Stationary.DTO.InputModels;

/// <summary>
/// Набор параметров тестирования
/// </summary>
public record SolutionParameters
{
    /// <summary>
    /// Значение коэффициента мю
    /// </summary>
    public double Mu { get; init; }

    /// <summary>
    /// Значение коэффициента гамма
    /// </summary>
    public double Gamma { get; init; }

    /// <summary>
    /// Краевое условие
    /// </summary>
    public EBoundaryConditions BoundaryCondition { get; init; }
}
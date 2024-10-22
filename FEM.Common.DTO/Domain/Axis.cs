using FEM.Common.DTO.Enums;
using FEM.Common.DTO.Models.InputModels;

namespace FEM.Common.DTO.Domain;

/// <summary>
/// Набор входных параметров построения сетки
/// </summary>
public record Axis
{
    /// <summary>
    /// Параметры геометрической параметризации расчетной области
    /// </summary>
    public required Positioning Positioning { get; init; }

    /// <summary>
    /// Дробление сетки
    /// </summary>
    public required Splitting Splitting { get; init; }

    /// <summary>
    /// Краевое условие
    /// </summary>
    public EBoundaryConditions BoundaryCondition { get; init; }
}
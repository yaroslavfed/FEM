using FEM.Common.Data.InputModels;

namespace FEM.Common.Data.Domain;

/// <summary>
/// Параметры физического объекта
/// </summary>
public record Strata
{
    /// <summary>
    /// Параметры геометрической параметризации физического объекта
    /// </summary>
    public required Positioning Positioning { get; init; }

    /// <summary>
    /// Плотность физического объекта
    /// </summary>
    public required double Mu { get; set; }
}
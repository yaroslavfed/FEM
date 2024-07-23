using FEM.Common.Data.InputModels;

namespace FEM.Common.Data.Domain;

/// <summary>
/// Набор входных параметров построения сетки
/// </summary>
public record Axis
{
    /// <summary>
    /// Дополнительные параметры векторного МКЭ
    /// </summary>
    public required AdditionalParameters AdditionalParameters { get; init; }

    /// <summary>
    /// Параметры геометрической параметризации расчетной области
    /// </summary>
    public required Positioning Positioning { get; init; }

    /// <summary>
    /// Дробление сетки
    /// </summary>
    public required Splitting Splitting { get; init; }

    /// <summary>
    /// Дополнительные настройки тестирования
    /// </summary>
    public required TestingSettings TestingSettings { get; init; }
}
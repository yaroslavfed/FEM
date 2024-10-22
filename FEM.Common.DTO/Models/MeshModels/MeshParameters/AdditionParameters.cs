namespace FEM.Common.DTO.Models.MeshModels.MeshParameters;

/// <summary>
/// Дополнительные параметры решения
/// </summary>
public record AdditionParameters
{
    /// <summary>
    /// Коэффициент мю
    /// </summary>
    public double MuCoefficient { get; init; }

    /// <summary>
    /// Коэффициент гамма
    /// </summary>
    public double GammaCoefficient { get; init; }
}
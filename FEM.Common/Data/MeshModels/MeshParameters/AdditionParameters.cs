namespace FEM.Common.Data.MeshModels.MeshParameters;

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

    /// <summary>
    /// Номер краевого условия
    /// </summary>
    public int BoundaryCondition { get; init; }
}
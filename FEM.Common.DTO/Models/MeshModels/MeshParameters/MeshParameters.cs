namespace FEM.Common.DTO.Models.MeshModels.MeshParameters;

/// <summary>
/// Параметры задания координатной сетки расчётной области
/// </summary>
public record MeshParameters
{
    /// <summary>
    /// Центр расчётной области по OX
    /// </summary>
    public double XCenterCoordinate { get; init; }

    /// <summary>
    /// Расстрояние до границы расчётной области по OX
    /// </summary>
    public double XStepToBounds { get; init; }

    /// <summary>
    /// Центр расчётной области по OY
    /// </summary>
    public double YCenterCoordinate { get; init; }

    /// <summary>
    /// Расстрояние до границы расчётной области по OY
    /// </summary>
    public double YStepToBounds { get; init; }

    /// <summary>
    /// Центр расчётной области по OZ
    /// </summary>
    public double ZCenterCoordinate { get; init; }

    /// <summary>
    /// Расстрояние до границы расчётной области по OZ
    /// </summary>
    public double ZStepToBounds { get; init; }
}
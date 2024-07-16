using FEM.Common.Data.MathModels;

namespace FEM.Common.Data.InputModels;

public record Splitting
{
    public required Point3D SplittingCoefficient { get; init; }

    public required Point3D MultiplyCoefficient { get; init; }
}
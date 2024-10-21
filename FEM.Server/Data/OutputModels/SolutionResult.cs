using FEM.Common.Data.MathModels;
using FEM.Common.Data.TestingContext;

namespace FEM.Server.Data.OutputModels;

public record SolutionResult
{
    public Vector? Solve { get; init; }

    public SolutionAdditionalInfo? SolutionInfo { get; set; }

    public int ItersCount { get; init; }
}
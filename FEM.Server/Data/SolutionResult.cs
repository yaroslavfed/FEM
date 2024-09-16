using FEM.Common.Data.MathModels;
using FEM.Common.Data.TestSession;

namespace FEM.Server.Data;

public record SolutionResult
{
    public Vector? Solve { get; init; }

    public SolutionAdditionalInfo? SolutionInfo { get; set; }

    public int ItersCount { get; init; }
}
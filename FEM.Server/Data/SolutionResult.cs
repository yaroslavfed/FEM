using FEM.Common.Data.MathModels;
using FEM.Common.Data.TestSession;
using Vector = FEM.Server.Data.Domain.Vector;

namespace FEM.Server.Data;

public record SolutionResult
{
    public Vector? Solve { get; init; }

    public SolutionAdditionalInfo? SolutionInfo { get; set; }

    public int ItersCount { get; init; }
}
using FEM.Common.DTO.Abstractions;
using FEM.Common.DTO.TestingContext;

namespace FEM.Stationary.DTO.TestingContext;

public record StationaryTestResult : TestResultBase
{
    public IEnumerable<string> Plots { get; init; } = [];

    public int ItersCount { get; set; }

    public SolutionAdditionalInfo? SolutionInfo { get; set; }
}
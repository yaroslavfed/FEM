using FEM.SharedDTO.Abstractions;
using FEM.SharedDTO.TestingContext;

namespace Stationary.DTO.TestingContext;

public record StationaryTestResult : TestResultBase
{
    public IEnumerable<string> Plots { get; init; } = [];

    public int ItersCount { get; set; }

    public SolutionAdditionalInfo? SolutionInfo { get; set; }
}
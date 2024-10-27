using FEM.SharedDTO.Abstractions;
using FEM.SharedDTO.TestingContext;

namespace NonStationary.DTO.TestingContext;

public record NonStationaryTestResult : TestResultBase
{
    public IEnumerable<string> Plots { get; init; } = [];

    public int ItersCount { get; set; }

    public SolutionAdditionalInfo? SolutionInfo { get; set; }
}
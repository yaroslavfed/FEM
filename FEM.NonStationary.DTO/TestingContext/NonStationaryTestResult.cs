using FEM.Common.Data.TestingContext;

namespace FEM.NonStationary.DTO.TestingContext;

public record NonStationaryTestResult
{
    public Guid Id { get; init; }

    public IEnumerable<string> Plots { get; init; } = [];

    public int ItersCount { get; set; }

    public SolutionAdditionalInfo? SolutionInfo { get; set; }
}
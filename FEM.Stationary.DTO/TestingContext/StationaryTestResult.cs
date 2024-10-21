using FEM.Common.Data.TestingContext;

namespace FEM.Stationary.DTO.TestingContext;

public record StationaryTestResult
{
    public Guid Id { get; init; }

    public IEnumerable<string> Plots { get; init; } = [];

    public int ItersCount { get; set; }

    public SolutionAdditionalInfo? SolutionInfo { get; set; }
}
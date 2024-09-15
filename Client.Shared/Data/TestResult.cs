namespace Client.Shared.Data;

public record TestResult
{
    public Guid Id { get; init; }

    public IEnumerable<string> Plots { get; init; } = [];

    public int ItersCount { get; set; }

    public double Inaccuracy { get; set; } = 0;

    public IEnumerable<double> SolutionVector { get; set; } = [];
}
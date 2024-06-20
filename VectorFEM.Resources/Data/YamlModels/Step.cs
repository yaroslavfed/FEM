namespace VectorFEM.Resources.Data.YamlModels;

public record Step
{
    public double Hx { get; init; }
    public double Hy { get; init; }
    public double Hz { get; init; }
}
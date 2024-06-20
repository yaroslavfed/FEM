using YamlDotNet.Serialization;

namespace VectorFEM.Resources.Data.YamlModels;

public record Splitting
{
    [YamlMember(Alias = "step_count", ApplyNamingConventions = false)]
    public required Step StepCount { get; init; }

    [YamlMember(Alias = "kr", ApplyNamingConventions = false)]
    public required Kr Kr { get; init; }
}
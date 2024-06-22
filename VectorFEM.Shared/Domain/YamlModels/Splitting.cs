using YamlDotNet.Serialization;

namespace VectorFEM.Shared.Domain.YamlModels;

public record Splitting
{
    [YamlMember(Alias = "step_count", ApplyNamingConventions = false)]
    public required Step StepCount { get; init; }
    
    public required Kr Kr { get; init; }
}
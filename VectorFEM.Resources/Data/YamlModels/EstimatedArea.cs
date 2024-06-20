using VectorFEM.Data;
using YamlDotNet.Serialization;

namespace VectorFEM.Resources.Data.YamlModels;

public record EstimatedArea
{
    [YamlMember(Alias = "low_point", ApplyNamingConventions = false)]
    public required Node LowPoint { get; init; }

    [YamlMember(Alias = "high_point", ApplyNamingConventions = false)]
    public required Node HighPoint { get; init; }
}
using YamlDotNet.Serialization;

namespace VectorFEM.Resources.Data.YamlModels;

public record Kr
{
    [YamlMember(Alias = "kr_x", ApplyNamingConventions = false)]
    public double X { get; init; }

    [YamlMember(Alias = "kr_y", ApplyNamingConventions = false)]
    public double Y { get; init; }

    [YamlMember(Alias = "kr_z", ApplyNamingConventions = false)]
    public double Z { get; init; }
}
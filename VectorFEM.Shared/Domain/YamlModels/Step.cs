using YamlDotNet.Serialization;

namespace VectorFEM.Shared.Domain.YamlModels;

public record Step
{
    [YamlMember(Alias = "h_x", ApplyNamingConventions = false)]
    public double Hx { get; init; }
    
    [YamlMember(Alias = "h_y", ApplyNamingConventions = false)]
    public double Hy { get; init; }
    
    [YamlMember(Alias = "h_z", ApplyNamingConventions = false)]
    public double Hz { get; init; }
}
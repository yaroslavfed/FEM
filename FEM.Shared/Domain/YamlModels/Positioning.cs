using FEM.Shared.Domain.MathModels;
using YamlDotNet.Serialization;

namespace FEM.Shared.Domain.YamlModels;

public record Positioning
{
    public required Point3D Coordinate { get; init; }
    
    [YamlMember(Alias = "bounds_distance", ApplyNamingConventions = false)]
    public required Point3D BoundsDistance { get; init; }
}
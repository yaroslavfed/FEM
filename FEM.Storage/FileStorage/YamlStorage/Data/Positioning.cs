using FEM.Shared.Domain.MathModels;
using YamlDotNet.Serialization;

namespace FEM.Storage.FileStorage.YamlStorage.Data;

public abstract record Positioning
{
    [YamlMember(Alias = "coordinate", ApplyNamingConventions = false)]
    public required Point3D Coordinate { get; init; }
    
    [YamlMember(Alias = "bounds_distance", ApplyNamingConventions = false)]
    public required Point3D BoundsDistance { get; init; }
}
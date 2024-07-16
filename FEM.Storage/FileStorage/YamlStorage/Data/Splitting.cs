using FEM.Common.Data.MathModels;
using YamlDotNet.Serialization;

namespace FEM.Storage.FileStorage.YamlStorage.Data;

public record Splitting
{
    [YamlMember(Alias = "splitting_coefficient", ApplyNamingConventions = false)]
    public required Point3D StepCount { get; init; }

    [YamlMember(Alias = "multiply_coefficient", ApplyNamingConventions = false)]
    public required Point3D Kr { get; init; }
}
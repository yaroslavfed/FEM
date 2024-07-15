using YamlDotNet.Serialization;

namespace FEM.Storage.FileStorage.YamlStorage.Data;

public record TestingSettings
{
    [YamlMember(Alias = "test_function_number", ApplyNamingConventions = false)]
    public int Function { get; init; }
}
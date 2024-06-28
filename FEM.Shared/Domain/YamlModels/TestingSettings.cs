using YamlDotNet.Serialization;

namespace FEM.Shared.Domain.YamlModels;

public record TestingSettings
{
    [YamlMember(Alias = "test_function_number", ApplyNamingConventions = false)]
    public int Function { get; init; }
}
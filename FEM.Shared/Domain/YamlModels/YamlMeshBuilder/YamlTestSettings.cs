namespace FEM.Shared.Domain.YamlModels.YamlMeshBuilder;

public record YamlTestSettings
{
    public AdditionalParameters? AdditionalParameters { get; init; }

    public TestingSettings? TestingSettings { get; init; }
}
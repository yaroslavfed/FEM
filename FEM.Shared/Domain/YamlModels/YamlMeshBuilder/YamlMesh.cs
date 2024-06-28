namespace FEM.Shared.Domain.YamlModels.YamlMeshBuilder;

public record YamlMesh
{
    public required Positioning Positioning { get; init; }

    public required Splitting Splitting { get; init; }
}
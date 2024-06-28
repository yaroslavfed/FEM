using FEM.Core.Extensions;
using FEM.Core.Storages;
using FEM.Shared.Domain.Data;
using FEM.Shared.Domain.MathModels;
using FEM.Shared.Domain.YamlModels.YamlExtensions;
using FEM.Shared.Domain.YamlModels.YamlMeshBuilder;

namespace FEM.Core.Services.MeshService;

public class MeshService : IMeshService
{
    private readonly IReadable<YamlMesh> _meshStorage;

    public MeshService(IReadable<YamlMesh> meshStorage)
    {
        _meshStorage = meshStorage;
    }

    public async Task<Mesh> GenerateMesh()
    {
        var yamlMeshModel = await _meshStorage.GetItemAsync();
        await ConfigureMesh(yamlMeshModel);

        // TODO: доделать генерацию сетки
        return new Mesh();
    }

    private Task<List<Point3D>> ConfigureMesh(YamlMesh yamlMeshModel)
    {
        var x = new List<double>().ToList().SplitAxis(
            yamlMeshModel.Splitting.Kr.X,
            (int)yamlMeshModel.Splitting.StepCount.X,
            yamlMeshModel.Positioning.GetHighPoint3D().X,
            yamlMeshModel.Positioning.GetLowPoint3D().X
        );

        var y = new List<double>().ToList().SplitAxis(
            yamlMeshModel.Splitting.Kr.Y,
            (int)yamlMeshModel.Splitting.StepCount.Y,
            yamlMeshModel.Positioning.GetHighPoint3D().Y,
            yamlMeshModel.Positioning.GetLowPoint3D().Y
        );

        var z = new List<double>().ToList().SplitAxis(
            yamlMeshModel.Splitting.Kr.Z,
            (int)yamlMeshModel.Splitting.StepCount.Z,
            yamlMeshModel.Positioning.GetHighPoint3D().Z,
            yamlMeshModel.Positioning.GetLowPoint3D().Z
        );

        var strataMesh = (
                from itemZ in z
                from itemY in y
                from itemX in x
                select new Point3D
                {
                    X = itemX,
                    Y = itemY,
                    Z = itemZ
                })
            .ToList();

        return Task.FromResult(strataMesh);
    }
}
using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Core.Extensions;
using FEM.Storage.FileStorage;

namespace FEM.Core.Services.MeshService;

public class MeshService : IMeshService
{
    private readonly IReadableStorage _meshStorage;

    public MeshService(IReadableStorage meshStorage)
    {
        _meshStorage = meshStorage;
    }

    public async Task<Mesh> GenerateMesh()
    {
        var meshModel = await _meshStorage.GetAxisAsync();
        var result = await ConfigureMesh(meshModel);

        // HACK: вывод точек на консоль
        foreach (var point3D in result)
            Console.WriteLine(point3D.ToString());

        // TODO: доделать генерацию сетки
        return new();
    }

    private Task<List<Point3D>> ConfigureMesh(Axis yamlMeshModel)
    {
        var x = new List<double>().ToList()
                                  .SplitAxis(
                                      yamlMeshModel.Splitting.MultiplyCoefficient.X,
                                      (int)yamlMeshModel.Splitting.SplittingCoefficient.X,
                                      yamlMeshModel.Positioning.GetHighPoint3D().X,
                                      yamlMeshModel.Positioning.GetLowPoint3D().X
                                  );

        var y = new List<double>().ToList()
                                  .SplitAxis(
                                      yamlMeshModel.Splitting.MultiplyCoefficient.Y,
                                      (int)yamlMeshModel.Splitting.SplittingCoefficient.Y,
                                      yamlMeshModel.Positioning.GetHighPoint3D().Y,
                                      yamlMeshModel.Positioning.GetLowPoint3D().Y
                                  );

        var z = new List<double>().ToList()
                                  .SplitAxis(
                                      yamlMeshModel.Splitting.MultiplyCoefficient.Z,
                                      (int)yamlMeshModel.Splitting.SplittingCoefficient.Z,
                                      yamlMeshModel.Positioning.GetHighPoint3D().Z,
                                      yamlMeshModel.Positioning.GetLowPoint3D().Z
                                  );

        var strataMesh = (
                from itemZ in z
                from itemY in y
                from itemX in x
                select new Point3D { X = itemX, Y = itemY, Z = itemZ })
            .ToList();

        return Task.FromResult(strataMesh);
    }
}
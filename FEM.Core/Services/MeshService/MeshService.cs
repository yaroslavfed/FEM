using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Core.Extensions;
using FEM.Core.Services.NumberingService.EdgesNumberingService;
using FEM.Core.Services.NumberingService.NodesNumberingService;
using FEM.Storage.FileStorage;
using FiniteElement = FEM.Core.Data.FiniteElement;

namespace FEM.Core.Services.MeshService;

public class MeshService : IMeshService
{
    private readonly IReadableStorage       _meshStorage;
    private readonly IEdgesNumberingService _edgesNumberingService;
    private readonly INodesNumberingService _nodesNumberingService;

    public MeshService(
        IReadableStorage meshStorage,
        IEdgesNumberingService edgesNumberingService,
        INodesNumberingService nodesNumberingService
    )
    {
        _meshStorage = meshStorage;
        _edgesNumberingService = edgesNumberingService;
        _nodesNumberingService = nodesNumberingService;
    }

    public async Task<Mesh> GenerateMeshAsync()
    {
        var meshModel = await _meshStorage.GetAxisAsync();
        var result = await ConfigurePointsListAsync(meshModel);

        var nx = result.Select(points => points.X).Distinct().ToArray().Length;
        var ny = result.Select(points => points.Y).Distinct().ToArray().Length;
        var nz = result.Select(points => points.Z).Distinct().ToArray().Length;

        var finiteElements = Enumerable
                             .Range(0, (nx - 1) * (ny - 1) * (nz - 1))
                             .Select(i => new FiniteElement())
                             .ToArray();

        await _nodesNumberingService.ConfigureGlobalNumbering(nx, ny, nz, finiteElements);
        await _edgesNumberingService.ConfigureGlobalNumbering(nx, ny, nz, finiteElements);

        return new();
    }

    /// <summary>
    /// Получение списка точек из параметров конфигурации расчетной области
    /// </summary>
    /// <param name="meshParameters">Входные параметры сетки</param>
    /// <returns>Список точек принадлежащих расчетной области</returns>
    private Task<List<Point3D>> ConfigurePointsListAsync(Axis meshParameters)
    {
        var x = new List<double>().ToList()
                                  .SplitAxis(
                                      meshParameters.Splitting.MultiplyCoefficient.X,
                                      (int)meshParameters.Splitting.SplittingCoefficient.X,
                                      meshParameters.Positioning.GetHighPoint3D().X,
                                      meshParameters.Positioning.GetLowPoint3D().X
                                  );

        var y = new List<double>().ToList()
                                  .SplitAxis(
                                      meshParameters.Splitting.MultiplyCoefficient.Y,
                                      (int)meshParameters.Splitting.SplittingCoefficient.Y,
                                      meshParameters.Positioning.GetHighPoint3D().Y,
                                      meshParameters.Positioning.GetLowPoint3D().Y
                                  );

        var z = new List<double>().ToList()
                                  .SplitAxis(
                                      meshParameters.Splitting.MultiplyCoefficient.Z,
                                      (int)meshParameters.Splitting.SplittingCoefficient.Z,
                                      meshParameters.Positioning.GetHighPoint3D().Z,
                                      meshParameters.Positioning.GetLowPoint3D().Z
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
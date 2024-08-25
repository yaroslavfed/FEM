using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Common.Extensions;
using FEM.Storage.FileStorage;
using VectorFEM.Core.Data.Parallelepipedal;
using VectorFEM.Core.Services.Parallelepipedal.DrawingMeshService;
using VectorFEM.Core.Services.Parallelepipedal.NumberingService.EdgesNumberingService;
using VectorFEM.Core.Services.Parallelepipedal.NumberingService.NodesNumberingService;

namespace VectorFEM.Core.Services.Parallelepipedal.MeshService;

/// <inheritdoc cref="IMeshService"/>
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

    public async Task<Axis> GenerateTestConfiguration() => await _meshStorage.GetAxisAsync();

    public async Task<Mesh> GenerateMeshAsync()
    {
        var meshModel = await GenerateTestConfiguration();
        var pointsList = await ConfigurePointsListAsync(meshModel);

        var nx = pointsList.Select(points => points.X).Distinct().ToArray().Length;
        var ny = pointsList.Select(points => points.Y).Distinct().ToArray().Length;
        var nz = pointsList.Select(points => points.Z).Distinct().ToArray().Length;

        var finiteElements = Enumerable
                             .Range(0, (nx - 1) * (ny - 1) * (nz - 1))
                             .Select(i => new FiniteElementWithNumerics())
                             .ToArray();

        await _nodesNumberingService.ConfigureGlobalNumbering(nx, ny, nz, finiteElements);
        await _edgesNumberingService.ConfigureGlobalNumbering(nx, ny, nz, finiteElements);

        var mesh = new Mesh
        {
            Elements = finiteElements
                       .Select(
                           element => new FiniteElement()
                           {
                               Edges = element
                                       .MapNodesEdges
                                       .Select(
                                           (associationPoints, edgeIndex) => new Edge
                                           {
                                               EdgeIndex = element.Edges[edgeIndex],
                                               Nodes =
                                               [
                                                   new()
                                                   {
                                                       NodeIndex = associationPoints.First,
                                                       Coordinate = new()
                                                       {
                                                           X = pointsList[associationPoints.First].X,
                                                           Y = pointsList[associationPoints.First].Y,
                                                           Z = pointsList[associationPoints.First].Z
                                                       }
                                                   },
                                                   new()
                                                   {
                                                       NodeIndex = associationPoints.Second,
                                                       Coordinate = new()
                                                       {
                                                           X = pointsList[associationPoints.Second].X,
                                                           Y = pointsList[associationPoints.Second].Y,
                                                           Z = pointsList[associationPoints.Second].Z
                                                       }
                                                   }
                                               ]
                                           }
                                       )
                                       .ToList()
                           }
                       )
                       .ToList()
        };

        return mesh;
    }

    /// <summary>
    /// Получение списка точек из параметров конфигурации расчетной области
    /// </summary>
    /// <param name="meshParameters">Входные параметры сетки</param>
    /// <returns>Список точек принадлежащих расчетной области</returns>
    private Task<List<Point3D>> ConfigurePointsListAsync(Axis meshParameters)
    {
        var x = new List<double>()
                .ToList()
                .SplitAxis(
                    meshParameters.Splitting.MultiplyCoefficient.X,
                    (int)meshParameters.Splitting.SplittingCoefficient.X,
                    meshParameters.Positioning.GetHighPoint3D().X,
                    meshParameters.Positioning.GetLowPoint3D().X
                );

        var y = new List<double>()
                .ToList()
                .SplitAxis(
                    meshParameters.Splitting.MultiplyCoefficient.Y,
                    (int)meshParameters.Splitting.SplittingCoefficient.Y,
                    meshParameters.Positioning.GetHighPoint3D().Y,
                    meshParameters.Positioning.GetLowPoint3D().Y
                );

        var z = new List<double>()
                .ToList()
                .SplitAxis(
                    meshParameters.Splitting.MultiplyCoefficient.Z,
                    (int)meshParameters.Splitting.SplittingCoefficient.Z,
                    meshParameters.Positioning.GetHighPoint3D().Z,
                    meshParameters.Positioning.GetLowPoint3D().Z
                );

        var strataMesh
            = (from itemZ in z from itemY in y from itemX in x select new Point3D { X = itemX, Y = itemY, Z = itemZ })
            .ToList();

        return Task.FromResult(strataMesh);
    }
}
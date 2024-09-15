using FEM.Common.Data.Domain;
using FEM.Common.Data.InputModels;
using FEM.Common.Data.MathModels;
using FEM.Common.Enums;
using FEM.Common.Extensions;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Services.Parallelepipedal.NumberingService.EdgesNumberingService;
using FEM.Server.Services.Parallelepipedal.NumberingService.NodesNumberingService;
using FEM.Storage.FileStorage;

namespace FEM.Server.Services.Parallelepipedal.MeshService;

/// <inheritdoc cref="IMeshService"/>
public class MeshService : IMeshService
{
    private readonly IJsonStorage _meshStorage;
    private readonly IEdgesNumberingService _edgesNumberingService;
    private readonly INodesNumberingService _nodesNumberingService;

    public MeshService(
        IJsonStorage meshStorage,
        IEdgesNumberingService edgesNumberingService,
        INodesNumberingService nodesNumberingService
    )
    {
        _meshStorage = meshStorage;
        _edgesNumberingService = edgesNumberingService;
        _nodesNumberingService = nodesNumberingService;
    }

    public async Task<Axis> GenerateTestConfiguration() => await _meshStorage.GetAxisAsync();

    public async Task<Axis> GenerateTestConfiguration(TestSession testSession)
    {
        var axis = new Axis()
        {
            Positioning = new Positioning()
            {
                Coordinate = new Point3D()
                {
                    X = testSession.MeshParameters.XCenterCoordinate,
                    Y = testSession.MeshParameters.YCenterCoordinate,
                    Z = testSession.MeshParameters.ZCenterCoordinate
                },
                BoundsDistance = new Point3D()
                {
                    X = testSession.MeshParameters.XStepToBounds,
                    Y = testSession.MeshParameters.YStepToBounds,
                    Z = testSession.MeshParameters.ZStepToBounds
                }
            },
            Splitting = new Splitting()
            {
                SplittingCoefficient = new Point3D()
                {
                    X = testSession.SplittingParameters.XSplittingCoefficient,
                    Y = testSession.SplittingParameters.YSplittingCoefficient,
                    Z = testSession.SplittingParameters.ZSplittingCoefficient
                },
                MultiplyCoefficient = new Point3D()
                {
                    X = testSession.SplittingParameters.XMultiplyCoefficient,
                    Y = testSession.SplittingParameters.YMultiplyCoefficient,
                    Z = testSession.SplittingParameters.ZMultiplyCoefficient
                }
            },
            AdditionalParameters = new AdditionalParameters()
            {
                Mu = testSession.AdditionParameters.MuCoefficient,
                Gamma = testSession.AdditionParameters.GammaCoefficient,
                BoundaryCondition = (EBoundaryConditions)testSession.AdditionParameters.BoundaryCondition
            }
        };

        return axis;
    }

    public async Task<Mesh> GenerateMeshAsync(Axis meshModel)
    {
        var pointsList = await ConfigurePointsListAsync(meshModel);

        var nx = pointsList.Select(points => points.X).Distinct().ToArray().Length;
        var ny = pointsList.Select(points => points.Y).Distinct().ToArray().Length;
        var nz = pointsList.Select(points => points.Z).Distinct().ToArray().Length;

        var finiteElements = Enumerable
            .Range(0, (nx - 1) * (ny - 1) * (nz - 1))
            .Select(_ => new FiniteElementWithNumerics())
            .ToArray();

        await _nodesNumberingService.ConfigureGlobalNumbering(nx, ny, nz, finiteElements);
        await _edgesNumberingService.ConfigureGlobalNumbering(nx, ny, nz, finiteElements);

        var mesh = new Mesh
        {
            Elements = finiteElements
                .Select(
                    element => new FiniteElement
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
    private static Task<List<Point3D>> ConfigurePointsListAsync(Axis meshParameters)
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
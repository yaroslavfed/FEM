using FEM.Common.DTO.Configurations.TestConfiguration;
using FEM.Common.DTO.Domain;
using FEM.Common.DTO.Enums;
using FEM.Common.DTO.Models.MathModels;
using FEM.Common.DTO.Models.MeshModels;
using FEM.Common.Extensions;
using FEM.Common.Services.NumberingService.EdgesNumberingService;
using FEM.Common.Services.NumberingService.NodesNumberingService;

namespace FEM.Common.Services.MeshService;

/// <inheritdoc cref="IMeshService"/>
public class MeshService : IMeshService
{
    private readonly IEdgesNumberingService _edgesNumberingService;
    private readonly INodesNumberingService _nodesNumberingService;

    public MeshService(IEdgesNumberingService edgesNumberingService, INodesNumberingService nodesNumberingService)
    {
        _edgesNumberingService = edgesNumberingService;
        _nodesNumberingService = nodesNumberingService;
    }

    /// <summary>
    /// Генерация модели параметров построения сетки
    /// </summary>
    /// <param name="testConfiguration"><see cref="TestConfigurationBase">Набор параметров тестирования</see></param>
    /// <returns>Набор входных параметров построения сетки</returns>
    public Task<Axis> GenerateTestConfiguration(TestConfigurationBase testConfiguration)
    {
        var axis = new Axis
        {
            Positioning
                = new()
                {
                    Coordinate = new()
                    {
                        X = testConfiguration.MeshParameters.XCenterCoordinate,
                        Y = testConfiguration.MeshParameters.YCenterCoordinate,
                        Z = testConfiguration.MeshParameters.ZCenterCoordinate
                    },
                    BoundsDistance
                        = new()
                        {
                            X = testConfiguration.MeshParameters.XStepToBounds,
                            Y = testConfiguration.MeshParameters.YStepToBounds,
                            Z = testConfiguration.MeshParameters.ZStepToBounds
                        }
                },
            Splitting = new()
            {
                SplittingCoefficient = new()
                {
                    X = testConfiguration.SplittingParameters.XSplittingCoefficient,
                    Y = testConfiguration.SplittingParameters.YSplittingCoefficient,
                    Z = testConfiguration.SplittingParameters.ZSplittingCoefficient
                },
                MultiplyCoefficient = new()
                {
                    X = testConfiguration.SplittingParameters.XMultiplyCoefficient,
                    Y = testConfiguration.SplittingParameters.YMultiplyCoefficient,
                    Z = testConfiguration.SplittingParameters.ZMultiplyCoefficient
                }
            },
            BoundaryCondition = (EBoundaryConditions)testConfiguration.BoundaryCondition
        };

        return Task.FromResult(axis);
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
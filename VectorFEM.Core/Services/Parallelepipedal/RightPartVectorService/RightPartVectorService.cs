using FEM.Common.Data.Domain;
using VectorFEM.Core.Data.Parallelepipedal;
using VectorFEM.Core.Extensions;
using FEM.Common.Enums;
using FEM.Common.Data.TestSession;
using VectorFEM.Core.Services.TestingService;

namespace VectorFEM.Core.Services.Parallelepipedal.RightPartVectorService;

// TODO: надо бы переделать класс, не нравится мне эта перегонка в локальные из глобальных, выглядит бесполезной
/// <inheritdoc cref="IRightPartVectorService"/>
public class RightPartVectorService : IRightPartVectorService
{
    private readonly ITestingService _testingService;

    public RightPartVectorService(ITestingService testingService)
    {
        _testingService = testingService;
    }

    public async Task<double> ResolveRightPartValueAsync(Edge edge, TestSession<Mesh> testSession)
    {
        var finiteElementIndex = await edge.FiniteElementIndexByEdges(testSession.Mesh);
        var localFiniteElement = testSession.Mesh.Elements[finiteElementIndex];

        var edgeIndex = await edge.ResolveLocal(localFiniteElement);
        var firstNode = localFiniteElement.Edges[edgeIndex].Nodes[0];
        var secondNode = localFiniteElement.Edges[edgeIndex].Nodes[1];

        var firstNodeIndex = await firstNode.ResolveLocal(localFiniteElement);
        var secondNodeIndex = await secondNode.ResolveLocal(localFiniteElement);

        var nodesList = localFiniteElement
                        .Edges
                        .SelectMany(item => item.Nodes)
                        .DistinctBy(node => node.NodeIndex)
                        .OrderBy(node => node.NodeIndex)
                        .ToList();

        var localFirstNode = new Node
        {
            Coordinate = new()
            {
                X = nodesList[firstNodeIndex].Coordinate.X,
                Y = nodesList[firstNodeIndex].Coordinate.Y,
                Z = nodesList[firstNodeIndex].Coordinate.Z
            }
        };

        var localSecondNode = new Node
        {
            Coordinate = new()
            {
                X = nodesList[secondNodeIndex].Coordinate.X,
                Y = nodesList[secondNodeIndex].Coordinate.Y,
                Z = nodesList[secondNodeIndex].Coordinate.Z
            }
        };

        var stepX = Math.Abs(localFirstNode.Coordinate.X - localSecondNode.Coordinate.X);
        if (stepX > 0)
            return await _testingService.ResolveVectorContributionsAsync(
                (firstNode, secondNode),
                testSession.Mu,
                testSession.Gamma,
                EDirections.OX
            );

        var stepY = Math.Abs(localFirstNode.Coordinate.Y - localSecondNode.Coordinate.Y);
        if (stepY > 0)
            return await _testingService.ResolveVectorContributionsAsync(
                (firstNode, secondNode),
                testSession.Mu,
                testSession.Gamma,
                EDirections.OY
            );

        var stepZ = Math.Abs(localFirstNode.Coordinate.Z - localSecondNode.Coordinate.Z);
        if (stepZ > 0)
            return await _testingService.ResolveVectorContributionsAsync(
                (firstNode, secondNode),
                testSession.Mu,
                testSession.Gamma,
                EDirections.OZ
            );

        return await _testingService.ResolveVectorContributionsAsync(
            (firstNode, secondNode),
            testSession.Mu,
            testSession.Gamma,
            EDirections.OX
        );
    }
}
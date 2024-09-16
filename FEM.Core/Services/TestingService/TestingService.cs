using FEM.Common.Data.Domain;
using FEM.Common.Data.TestSession;
using FEM.Common.Enums;
using FEM.Core.Data.Parallelepipedal;
using FEM.Core.Extensions;

namespace FEM.Core.Services.TestingService;

/// <inheritdoc cref="ITestingService"/>
public class TestingService : ITestingService
{
    public async Task<double> ResolveMatrixContributions(
        (Node firstNode, Node secondNode) nodesPair,
        EDirections direction
    )
    {
        var coordinate = (await CalculateNodeAsync(direction, nodesPair)).Coordinate;
        List<double> contributionsFromMatrixA =
        [
            35 * Math.Pow(coordinate.X, 3) + Math.Pow(coordinate.Y, 3) + Math.Pow(coordinate.Z, 3),
            Math.Pow(coordinate.X, 3) + 25 * Math.Pow(coordinate.Y, 3) + Math.Pow(coordinate.Z, 3),
            Math.Pow(coordinate.X, 3) + Math.Pow(coordinate.Y, 3) + 45 * Math.Pow(coordinate.Z, 3)
        ];

        return direction switch
        {
            EDirections.Ox => contributionsFromMatrixA[0],
            EDirections.Oy => contributionsFromMatrixA[1],
            EDirections.Oz => contributionsFromMatrixA[2],
            _              => throw new NotImplementedException()
        };
    }

    public async Task<double> ResolveVectorContributionsAsync(
        (Node firstNode, Node secondNode) nodesPair,
        EDirections direction
    )
    {
        var coordinate = (await CalculateNodeAsync(direction, nodesPair)).Coordinate;
        List<double> contributionsFromVectorF =
        [
            8 * (coordinate.Y + coordinate.Z),
            8 * (coordinate.X + coordinate.Z),
            8 * (coordinate.X + coordinate.Y)
        ];

        return direction switch
        {
            EDirections.Ox => contributionsFromVectorF[0],
            EDirections.Oy => contributionsFromVectorF[1],
            EDirections.Oz => contributionsFromVectorF[2],
            _              => throw new NotImplementedException()
        };
    }

    public async Task<(Node firstNode, Node secondNode, EDirections direction)> ResolveLocalNodes(
        Edge edge,
        TestSession<Mesh> testSession
    )
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
        var stepY = Math.Abs(localFirstNode.Coordinate.Y - localSecondNode.Coordinate.Y);
        var stepZ = Math.Abs(localFirstNode.Coordinate.Z - localSecondNode.Coordinate.Z);

        var direction = EDirections.Ox;
        if (stepX > 0)
            direction = EDirections.Ox;
        else if (stepY > 0)
            direction = EDirections.Oy;
        else if (stepZ > 0)
            direction = EDirections.Oz;

        return (firstNode, secondNode, direction);
    }

    private static Task<Node> CalculateNodeAsync(EDirections direction, (Node firstNode, Node secondNode) nodesPair)
    {
        var result = direction switch
        {
            EDirections.Ox => nodesPair.firstNode with
            {
                Coordinate = nodesPair.firstNode.Coordinate with
                {
                    X = (nodesPair.firstNode.Coordinate.X + nodesPair.secondNode.Coordinate.X) / 2
                }
            },
            EDirections.Oy => nodesPair.firstNode with
            {
                Coordinate = nodesPair.firstNode.Coordinate with
                {
                    Y = (nodesPair.firstNode.Coordinate.Y + nodesPair.secondNode.Coordinate.Y) / 2
                }
            },
            EDirections.Oz => nodesPair.firstNode with
            {
                Coordinate = nodesPair.firstNode.Coordinate with
                {
                    Z = (nodesPair.firstNode.Coordinate.Z + nodesPair.secondNode.Coordinate.Z) / 2
                }
            },
            _ => throw new NotImplementedException()
        };

        return Task.FromResult(result);
    }
}
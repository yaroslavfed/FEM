using FEM.Common.Data.Domain;
using FEM.Common.Data.TestSession;
using FEM.Common.Enums;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Extensions;

namespace FEM.Server.Services.ProblemService;

/// <inheritdoc cref="IProblemService"/>
public class ProblemService : IProblemService
{
    private readonly IBasisFunctionProvider.IBasisFunctionProvider _basisFunctionProvider;

    public ProblemService(IBasisFunctionProvider.IBasisFunctionProvider basisFunctionProvider)
    {
        _basisFunctionProvider = basisFunctionProvider;
    }

    /// <inheritdoc />
    public Task<Matrix> AssembleElementStiffnessMatrixAsync(FiniteElement element)
    {
        const int edgeCount = 12;
        var localMatrix = new Matrix(edgeCount, edgeCount);

        var volume = element.GetSizes().X * element.GetSizes().Y * element.GetSizes().Z;
        var center = element.GetCenter();

        for (int i = 0; i < edgeCount; i++)
        {
            var curlI = _basisFunctionProvider.GetCurl(element, i, center);

            for (int j = 0; j < edgeCount; j++)
            {
                var curlJ = _basisFunctionProvider.GetCurl(element, j, center);

                var dot = curlI.Dot(curlJ);
                localMatrix[i, j] = (1.0 / element.Mu) * dot * volume;
            }
        }

        return Task.FromResult(localMatrix);
    }

    /// <inheritdoc />
    public Task<Vector> AssembleElementRightHandVectorAsync(FiniteElement element, IEnumerable<CurrentSegment> sources)
    {
        const int edgeCount = 12;
        var localVector = new Vector(edgeCount);

        var volume = element.GetSizes().X * element.GetSizes().Y * element.GetSizes().Z;

        foreach (var source in sources)
        {
            if (!element.Contains(source.Center)) continue;

            for (int i = 0; i < edgeCount; i++)
            {
                var basis = _basisFunctionProvider.GetValue(element, i, source.Center);
                double contribution = basis.Dot(source.Direction) * source.Current;

                localVector[i] += contribution * volume;
            }
        }

        return Task.FromResult(localVector);
    }

    /// <inheritdoc />
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
                    X = (nodesPair.firstNode.Coordinate.X + nodesPair.secondNode.Coordinate.X) / 2.0
                }
            },
            EDirections.Oy => nodesPair.firstNode with
            {
                Coordinate = nodesPair.firstNode.Coordinate with
                {
                    Y = (nodesPair.firstNode.Coordinate.Y + nodesPair.secondNode.Coordinate.Y) / 2.0
                }
            },
            EDirections.Oz => nodesPair.firstNode with
            {
                Coordinate = nodesPair.firstNode.Coordinate with
                {
                    Z = (nodesPair.firstNode.Coordinate.Z + nodesPair.secondNode.Coordinate.Z) / 2.0
                }
            },
            _ => throw new NotImplementedException()
        };

        return Task.FromResult(result);
    }
}
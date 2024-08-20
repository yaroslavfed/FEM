using FEM.Common.Data.Domain;
using VectorFEM.Core.Data.Parallelepipedal;
using VectorFEM.Core.Extensions;
using FEM.Common.Data.MathModels;
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

        var step = new Point3D
        {
            X = Math.Abs(localFirstNode.Coordinate.X - localSecondNode.Coordinate.X),
            Y = Math.Abs(localFirstNode.Coordinate.Y - localSecondNode.Coordinate.Y),
            Z = Math.Abs(localFirstNode.Coordinate.Z - localSecondNode.Coordinate.Z)
        };

        if (step.X > 0) return await ResolveFunctionContribution(firstNode, secondNode, testSession, EDirections.OX);
        if (step.Y > 0) return await ResolveFunctionContribution(firstNode, secondNode, testSession, EDirections.OY);
        if (step.Z > 0) return await ResolveFunctionContribution(firstNode, secondNode, testSession, EDirections.OZ);

        return await ResolveFunctionContribution(firstNode, secondNode, testSession, EDirections.OX);
    }

    private async Task<double> ResolveFunctionContribution(
        Node firstNode,
        Node secondNode,
        TestSession<Mesh> testSession,
        EDirections direction
    )
    {
        var result = direction switch
        {
            EDirections.OX => firstNode with
            {
                Coordinate = firstNode.Coordinate with
                {
                    X = (firstNode.Coordinate.X + secondNode.Coordinate.X) / 2
                }
            },
            EDirections.OY => firstNode with
            {
                Coordinate = firstNode.Coordinate with
                {
                    Y = (firstNode.Coordinate.Y + secondNode.Coordinate.Y) / 2
                }
            },
            EDirections.OZ => firstNode with
            {
                Coordinate = firstNode.Coordinate with
                {
                    Z = (firstNode.Coordinate.Z + secondNode.Coordinate.Z) / 2
                }
            },
            _ => throw new NotImplementedException()
        };

        return await _testingService.ResolveRightPartVector(
            result.Coordinate,
            testSession.Mu,
            testSession.Gamma,
            direction
        );
    }
}
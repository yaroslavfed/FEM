using FEM.Common.Data.Domain;
using FEM.Common.Enums;

namespace VectorFEM.Core.Services.TestingService;

/// <inheritdoc cref="ITestingService"/>
public class TestingService : ITestingService
{
    public async Task<double> ResolveMatrixContributions(
        (Node firstNode, Node secondNode) nodesPair,
        double gamma,
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

        return gamma
               * direction switch
               {
                   EDirections.OX => contributionsFromMatrixA[0],
                   EDirections.OY => contributionsFromMatrixA[1],
                   EDirections.OZ => contributionsFromMatrixA[2],
                   _              => throw new NotImplementedException()
               };
    }

    public async Task<double> ResolveVectorContributionsAsync(
        (Node firstNode, Node secondNode) nodesPair,
        double mu,
        double gamma,
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

        var contributionsFromMatrixA = await ResolveMatrixContributions(nodesPair, gamma, direction);

        return 1
               / mu
               * direction switch
               {
                   EDirections.OX => contributionsFromVectorF[0],
                   EDirections.OY => contributionsFromVectorF[1],
                   EDirections.OZ => contributionsFromVectorF[2],
                   _              => throw new NotImplementedException()
               }
               + contributionsFromMatrixA;
    }

    private static Task<Node> CalculateNodeAsync(EDirections direction, (Node firstNode, Node secondNode) nodesPair)
    {
        var result = direction switch
        {
            EDirections.OX => nodesPair.firstNode with
            {
                Coordinate = nodesPair.firstNode.Coordinate with
                {
                    X = (nodesPair.firstNode.Coordinate.X + nodesPair.secondNode.Coordinate.X) / 2
                }
            },
            EDirections.OY => nodesPair.firstNode with
            {
                Coordinate = nodesPair.firstNode.Coordinate with
                {
                    Y = (nodesPair.firstNode.Coordinate.Y + nodesPair.secondNode.Coordinate.Y) / 2
                }
            },
            EDirections.OZ => nodesPair.firstNode with
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
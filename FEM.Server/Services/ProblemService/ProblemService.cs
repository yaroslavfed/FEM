using System.Numerics;
using FEM.Common.Data.Domain;
using FEM.Common.Data.TestSession;
using FEM.Common.Enums;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Extensions;
using FEM.Server.Models.BasicFunction;
using FEM.Server.Models.CurrentSource;

namespace FEM.Server.Services.ProblemService;

/// <inheritdoc cref="IProblemService"/>
public class ProblemService : IProblemService
{
    private readonly IBasicFunction _basicFunction;

    public ProblemService(IBasicFunction basicFunction)
    {
        _basicFunction = basicFunction;
    }

    /// <inheritdoc />
    public async Task<double[,]> BuildElementStiffnessMatrixAsync(FiniteElement element)
    {
        const int edgeCount = 12; // В параллелепипеде 12 рёбер → 12 базисных функций

        var matrix = new double[edgeCount, edgeCount];

        // Предполагаем, что у нас есть метод GetBasisFunctionCurl(i) → Vector3[]
        // возвращающий значения ротора базисной функции φ_i в узлах элемента

        for (int i = 0; i < edgeCount; i++)
        {
            var curlI = await GetBasisFunctionCurlAsync(element, i); // List<Vector3> длины 8

            for (int j = 0; j < edgeCount; j++)
            {
                var curlJ = await GetBasisFunctionCurlAsync(element, j); // List<Vector3> длины 8

                double localIntegral = 0;

                // Простая квадратурная формула: усреднение по вершинам (или использовать формулу точнее)
                for (int k = 0; k < curlI.Count; k++)
                {
                    var dotProduct = Vector3.Dot(curlI[k], curlJ[k]);
                    localIntegral += dotProduct;
                }

                localIntegral /= curlI.Count;    // Среднее значение скалярного произведения роторов
                localIntegral /= element.Mu;     // Учитываем 1/mu
                localIntegral *= element.Volume; // Интеграл по объёму

                matrix[i, j] = localIntegral;
            }
        }

        return matrix;
    }

    /// <inheritdoc />
    public async Task<double[]> BuildElementRightHandVectorAsync(
        FiniteElement element,
        IEnumerable<ICurrentSource> currentSources
    )
    {
        const int edgeCount = 12;
        var rhs = new double[edgeCount];

        // Получаем центр и объём элемента
        var center = element.Center;
        var volume = element.Volume;

        foreach (var source in currentSources)
        {
            // Проверка: влияет ли токовый источник на этот элемент
            if (!source.Intersects(element.BoundingBox))
                continue;

            // Получаем токовую плотность J в центре элемента
            var J = await source.EvaluateCurrentDensityAsync(center);

            for (int i = 0; i < edgeCount; i++)
            {
                var phi = await GetBasisFunctionAsync(element, i, center); // Векторная функция в центре
                var dot = Vector3.Dot(J, phi);

                rhs[i] += dot * volume; // Интеграл приближён квадратурой
            }
        }

        return rhs;
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
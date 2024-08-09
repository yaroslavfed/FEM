using FEM.Common.Data.Domain;
using VectorFEM.Core.Data.Parallelepipedal;
using VectorFEM.Core.Extensions;
using FEM.Common.Data.MathModels;
using FEM.Common.Enums;
using System.Drawing;

namespace VectorFEM.Core.Services.Parallelepipedal.RightPartVectorService;

// TODO: РЅР°РґРѕ Р±С‹ РїРµСЂРµРґРµР»Р°С‚СЊ РєР»Р°СЃСЃ, РЅРµ РЅСЂР°РІРёС‚СЃСЏ РјРЅРµ СЌС‚Р° РїРµСЂРµРіРѕРЅРєР° РІ Р»РѕРєР°Р»СЊРЅС‹Рµ РёР· РіР»РѕР±Р°Р»СЊРЅС‹С…, РІС‹РіР»СЏРґРёС‚ Р±РµСЃРїРѕР»РµР·РЅРѕР№
/// <inheritdoc cref="IRightPartVectorService"/>
public class RightPartVectorService : IRightPartVectorService
{
    public async Task<double> ResolveRightPartValueAsync(Edge edge, Mesh strata){
        int finiteElementIndex = await edge.FiniteElementIndexByEdges(strata);
        var localFiniteElement = strata.Elements[finiteElementIndex];

        int edgeIndex = await edge.ResolveLocal(localFiniteElement);
        var firstNode = localFiniteElement.Edges[edgeIndex].Nodes[0];
        var secondNode = localFiniteElement.Edges[edgeIndex].Nodes[1];

        int firstNodeIndex = await firstNode.ResolveLocal(localFiniteElement);
        int secondNodeIndex = await secondNode.ResolveLocal(localFiniteElement);

        var nodesList = localFiniteElement.Edges.SelectMany(edge => edge.Nodes).Order().ToList();
        var localFirstNode = new Node
        {
            Coordinate = new Point3D
            {
                X = nodesList[firstNodeIndex].Coordinate.X,
                Y = nodesList[firstNodeIndex].Coordinate.Y,
                Z = nodesList[firstNodeIndex].Coordinate.Z
            }
        };

        var localSecondNode = new Node
        {
            Coordinate = new Point3D
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

        if (step.X > 0) await HalvePointAsync(firstNode, secondNode, EDirections.OX);
        else if (step.Y > 0) await HalvePointAsync(firstNode, secondNode, EDirections.OY);
        else if (step.Z > 0) await HalvePointAsync(firstNode, secondNode, EDirections.OZ);
        else await HalvePointAsync(firstNode, secondNode, EDirections.OX);

        
    }

    private Task<Node> HalvePointAsync(Node firstNode, Node secondNode, EDirections direction)
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

        return Task.FromResult(result);
    }
}
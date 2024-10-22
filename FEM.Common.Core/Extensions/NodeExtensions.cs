using FEM.Common.DTO.Domain;
using FEM.Common.DTO.Models.MeshModels;

namespace FEM.Common.Core.Extensions;

/// <summary>
/// Расширения для узлов расчётной области
/// </summary>
public static class NodeExtensions
{
    /// <summary>
    /// Получение номера локального узла на конечном элементе
    /// </summary>
    /// <param name="node">Узел в расчётной области</param>
    /// <param name="element">Рассматриваемый конечный элемент</param>
    /// <returns>Номер узла</returns>
    public static Task<int> ResolveLocal(this Node node, FiniteElement element)
    {
        var nodes = element.Edges.SelectMany(edge => edge.Nodes).ToArray();
        for (var i = 0; i < 8; i++)
            if (node.NodeIndex == nodes[i].NodeIndex)
                return Task.FromResult(i);

        return Task.FromResult(0);
    }
}
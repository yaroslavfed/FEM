using FEM.Common.DTO.Domain;
using FEM.Common.DTO.Models.MeshModels;

namespace FEM.Common.Core.Extensions;

/// <summary>
/// Расширения для ребер расчётной области
/// </summary>
public static class EdgeExtensions
{
    /// <summary>
    /// Получение номера локального ребра на конечном элементе
    /// </summary>
    /// <param name="edge">Ребро в расчётной области</param>
    /// <param name="element">Рассматриваемый конечный элемент</param>
    /// <returns>Номер ребра</returns>
    public static Task<int> ResolveLocal(this Edge edge, FiniteElement element)
    {
        var edges = element.Edges;
        for (var i = 0; i < 12; i++)
            if (edge.EdgeIndex == edges[i].EdgeIndex)
                return Task.FromResult(i);

        return Task.FromResult(0);
    }

    /// <summary>
    /// Получение номера конечного элемента по его ребрам
    /// </summary>
    /// <param name="edge">Ребра конечного элемента</param>
    /// <param name="mesh">Сетка расчётной области</param>
    /// <returns>Номер конечного элемента</returns>
    public static Task<int> FiniteElementIndexByEdges(this Edge edge, Mesh mesh)
    {
        for (var i = 0; i < mesh.Elements.Count; i++)
        {
            for (var j = 0; j < 12; j++)
            {
                if (mesh.Elements[i].Edges[j].EdgeIndex == edge.EdgeIndex)
                {
                    return Task.FromResult(i);
                }
            }
        }

        return Task.FromResult(0);
    }
}
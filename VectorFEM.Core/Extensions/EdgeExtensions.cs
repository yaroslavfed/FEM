using FEM.Common.Data.Domain;
using VectorFEM.Core.Data.Parallelepipedal;

namespace VectorFEM.Core.Extensions;

public static class EdgeExtensions
{
    public static Task<int> ResolveLocal(this Edge edge, FiniteElement element)
    {
        var edges = element.Edges;
        for (var i = 0; i < 12; i++)
            if (edge.EdgeIndex == edges[i].EdgeIndex)
                return Task.FromResult(i);

        return Task.FromResult(0);
    }

    public static Task<int> StrataByEdges(this Edge edge, Mesh strata)
    {
        for (int i = 0; i < strata.Elements.Count; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (strata.Elements[i].Edges[j].EdgeIndex == edge.EdgeIndex)
                {
                    return Task.FromResult(i);
                }
            }
        }

        return Task.FromResult(0);
    }
}
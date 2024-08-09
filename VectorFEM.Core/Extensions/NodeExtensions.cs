﻿using FEM.Common.Data.Domain;
using VectorFEM.Core.Data.Parallelepipedal;

namespace VectorFEM.Core.Extensions;

public static class NodeExtensions
{
    public static Task<int> ResolveLocal(this Node node, FiniteElement element)
    {
        var nodes = element.Edges.SelectMany(edge => edge.Nodes).ToArray();
        for (var i = 0; i < 8; i++)
            if (node.NodeIndex == nodes[i].NodeIndex)
                return Task.FromResult(i);

        return Task.FromResult(0);
    }
}
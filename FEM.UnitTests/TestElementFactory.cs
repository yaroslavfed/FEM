using FEM.Common.Data.Domain;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.UnitTests;

public static class TestElementFactory
{
    public static FiniteElement CreateUnitCube(double mu)
    {
        var nodes = new List<Node>();
        int index = 0;
        for (int z = 0; z <= 1; z++)
            for (int y = 0; y <= 1; y++)
                for (int x = 0; x <= 1; x++)
                    nodes.Add(new() { NodeIndex = index++, Coordinate = new(x, y, z) });

        // упрощённо создаём рёбра (12 штук), каждая из двух узлов
        var edges = Enumerable
                    .Range(0, 12)
                    .Select(i => new Edge
                        {
                            EdgeIndex = i, Nodes = [nodes[i % nodes.Count], nodes[(i + 1) % nodes.Count]]
                        }
                    )
                    .ToList();

        return new() { Edges = edges, Mu = mu };
    }
}
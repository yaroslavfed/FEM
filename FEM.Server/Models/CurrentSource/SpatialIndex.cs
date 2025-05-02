using System.Numerics;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Models.CurrentSource;

public class SpatialIndex
{
    private readonly Dictionary<(int x, int y, int z), List<FiniteElement>> _grid = new();
    private readonly double                                                 _cellSize;

    public SpatialIndex(IEnumerable<FiniteElement> elements, double cellSize = 1.0)
    {
        _cellSize = cellSize;

        foreach (var el in elements)
        {
            var aabb = GetElementAABB(el);
            var min = GetCellIndex(new((float)aabb.min.X, (float)aabb.min.Y, (float)aabb.min.Z));
            var max = GetCellIndex(new((float)aabb.max.X, (float)aabb.max.Y, (float)aabb.max.Z));

            for (int i = min.x; i <= max.x; i++)
                for (int j = min.y; j <= max.y; j++)
                    for (int k = min.z; k <= max.z; k++)
                    {
                        var key = (i, j, k);
                        if (!_grid.TryGetValue(key, out var list))
                        {
                            list = [];
                            _grid[key] = list;
                        }

                        list.Add(el);
                    }
        }
    }

    public FiniteElement? FindClosestElement(Vector3 point)
    {
        var key = GetCellIndex(point);
        if (!_grid.TryGetValue(key, out var candidates))
            return null;

        FiniteElement? closest = null;
        double minDist = double.MaxValue;

        foreach (var el in candidates)
        {
            var center = GetElementCenter(el);
            double dist = (center - point).LengthSquared();
            if (dist < minDist)
            {
                minDist = dist;
                closest = el;
            }
        }

        return closest;
    }

    private (int x, int y, int z) GetCellIndex(Vector3 point)
    {
        return ((int)Math.Floor(point.X / _cellSize), (int)Math.Floor(point.Y / _cellSize),
                (int)Math.Floor(point.Z / _cellSize));
    }

    private ((double X, double Y, double Z) min, (double X, double Y, double Z) max) GetElementAABB(FiniteElement el)
    {
        var allNodes = el.Edges.SelectMany(e => e.Nodes).Select(n => n.Coordinate).Distinct().ToList();

        var min = (X: allNodes.Min(n => n.X), Y: allNodes.Min(n => n.Y), Z: allNodes.Min(n => n.Z));
        var max = (X: allNodes.Max(n => n.X), Y: allNodes.Max(n => n.Y), Z: allNodes.Max(n => n.Z));

        return (min, max);
    }

    private Vector3 GetElementCenter(FiniteElement el)
    {
        var coords = el.Edges.SelectMany(e => e.Nodes).Select(n => n.Coordinate).Distinct().ToList();

        float x = (float)coords.Average(p => p.X);
        float y = (float)coords.Average(p => p.Y);
        float z = (float)coords.Average(p => p.Z);

        return new Vector3(x, y, z);
    }
}
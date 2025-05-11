using FEM.Common.Data.MathModels;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.Static.IntegrationHelper;

public static class IntegrationHelper
{
    public static List<Point3D> GetIntegrationPoints(FiniteElement element)
    {
        var nodes = element.Edges.SelectMany(e => e.Nodes).DistinctBy(n => n.NodeIndex).ToList();

        var minX = nodes.Min(n => n.Coordinate.X);
        var maxX = nodes.Max(n => n.Coordinate.X);
        var minY = nodes.Min(n => n.Coordinate.Y);
        var maxY = nodes.Max(n => n.Coordinate.Y);
        var minZ = nodes.Min(n => n.Coordinate.Z);
        var maxZ = nodes.Max(n => n.Coordinate.Z);

        var centerX = (minX + maxX) / 2.0;
        var centerY = (minY + maxY) / 2.0;
        var centerZ = (minZ + maxZ) / 2.0;

        var points = new List<Point3D>();

        foreach (var x in new[]
        {
            minX,
            centerX
        })
            foreach (var y in new[]
            {
                minY,
                centerY
            })
                foreach (var z in new[]
                {
                    minZ,
                    centerZ
                })
                {
                    points.Add(new Point3D(x + (maxX - minX) / 4, y + (maxY - minY) / 4, z + (maxZ - minZ) / 4));
                }

        return points;
    }
}
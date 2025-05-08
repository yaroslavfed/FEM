using System.Text.Json;
using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Server.Data.Domain;
using Vector = FEM.Server.Data.Domain.Vector;

namespace FEM.Server.Services.SolutionExportService;

public class SolutionExportService:ISolutionExportService
{
    public void ExportToJson(
        IReadOnlyList<Edge> edges,
        Vector solution,
        string filePath)
    {
        var data = edges.Select((edge, i) =>
        {
            var node1 = edge.Nodes[0].Coordinate;
            var node2 = edge.Nodes[1].Coordinate;

            var center = new Point3D
            {
                X = (node1.X + node2.X) / 2.0,
                Y = (node1.Y + node2.Y) / 2.0,
                Z = (node1.Z + node2.Z) / 2.0
            };

            var direction = new Vector3D
            {
                X = node2.X - node1.X,
                Y = node2.Y - node1.Y,
                Z = node2.Z - node1.Z
            }.Normalize();

            return new
            {
                x = center.X,
                y = center.Y,
                z = center.Z,
                dx = direction.X,
                dy = direction.Y,
                dz = direction.Z,
                value = solution[i]
            };
        });

        var jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(data, jsonOptions);
        File.WriteAllText(filePath, json);
    }
}
using FEM.Common.Data.Domain;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Services.BasisFunctionProvider;

namespace FEM.Server.Services.SolutionExportService;

public interface ISolutionExportService
{
    void ExportToJson(IReadOnlyList<Edge> edges, Vector solution, string filePath);

    void ExportFullBFieldToJson(IReadOnlyList<Sensor> sensors, Mesh mesh, Vector solution, string filePath);

    void ExportBFieldToVtu(IReadOnlyList<Sensor> sensors, Mesh mesh, Vector solution, string filePath);

    void ExportSensorsToJson(IReadOnlyList<Sensor> sensors, Mesh mesh, Vector solution, string filePath);
}
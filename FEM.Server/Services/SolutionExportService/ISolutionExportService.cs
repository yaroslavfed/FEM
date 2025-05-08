using FEM.Common.Data.Domain;
using FEM.Server.Data.Domain;

namespace FEM.Server.Services.SolutionExportService;

public interface ISolutionExportService
{
    void ExportToJson(
        IReadOnlyList<Edge> edges,
        Vector solution,
        string filePath);
}
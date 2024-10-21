using FEM.Common.Data.MeshModels;

namespace FEM.Server.Services.Parallelepipedal.VisualizerService;

public interface IVisualizerService
{
    Task DrawMeshPlotAsync(Mesh mesh);

    Task WriteMatrixToFileAsync(IMatrixFormat matrixProfile);
}
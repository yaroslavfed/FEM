using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.VisualizerService;

public interface IVisualizerService
{
    Task DrawMeshPlotAsync(Mesh mesh);

    Task WriteMatrixToFileAsync(IMatrixFormat matrixProfile);
}
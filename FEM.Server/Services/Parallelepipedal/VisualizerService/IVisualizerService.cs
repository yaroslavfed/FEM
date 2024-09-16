using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.Parallelepipedal.VisualizerService;

public interface IVisualizerService
{
    Task DrawMeshPlotAsync(Mesh mesh);

    Task WriteMatrixToFileAsync(IMatrixFormat matrixProfile);
}
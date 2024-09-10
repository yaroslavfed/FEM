using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.Parallelepipedal.DrawingMeshService;

public interface IVisualizerService
{
    Task DrawMeshPlotAsync(Mesh mesh);

    Task WriteMatrixToFileAsync(IMatrixFormat matrixProfile);
}
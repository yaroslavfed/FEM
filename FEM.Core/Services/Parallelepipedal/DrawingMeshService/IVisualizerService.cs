using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Core.Data.Parallelepipedal;

namespace FEM.Core.Services.Parallelepipedal.DrawingMeshService;

public interface IVisualizerService
{
    Task DrawMeshPlotAsync(Mesh mesh);

    Task WriteMatrixToFileAsync(IMatrixFormat matrixProfile);
}
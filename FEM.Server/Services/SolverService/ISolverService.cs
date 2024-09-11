using FEM.Common.Data.MathModels;
using FEM.Common.Data.MathModels.MatrixFormats;

namespace FEM.Server.Services.SolverService;

public interface ISolverService
{
    Task<(Vector solve, double discrepancy, int iterCount)> GetSolutionVectorAsync(IMatrixFormat matrixFormat,
        int maxIterationsCount, double eps);
}
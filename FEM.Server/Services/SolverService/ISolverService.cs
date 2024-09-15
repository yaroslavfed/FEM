using FEM.Common.Data.MathModels;
using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Server.Data;

namespace FEM.Server.Services.SolverService;

public interface ISolverService
{
    Task<SolutionResult> GetSolutionVectorAsync(IMatrixFormat matrixFormat, int maxIterationsCount, double eps);
}
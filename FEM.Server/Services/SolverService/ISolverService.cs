using FEM.Common.Data.MathModels;
using FEM.Server.Data;
using FEM.Server.Data.OutputModels;

namespace FEM.Server.Services.SolverService;

public interface ISolverService
{
    Task<SolutionResult> GetSolutionVectorAsync(IMatrixFormat matrixFormat, int maxIterationsCount, double eps);
}
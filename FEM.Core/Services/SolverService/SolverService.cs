using FEM.SharedDTO.Models.MatrixFormats;
using FEM.SharedDTO.Models.OutputModels;
using FEM.Solvers.Solvers;

namespace FEM.Core.Services.SolverService;

public class SolverService : ISolverService
{
    public Task<SolutionResult> GetSolutionVectorAsync(
        MatrixProfileFormat matrixFormat,
        int maxIterationsCount,
        double eps
    )
    {
        var solver = new LosLUSolver(maxIterationsCount, eps);
        var solveTuple = solver.Solve(matrixFormat);

        var result = new SolutionResult
        {
            Solve = solveTuple.solve, SolutionInfo = null, ItersCount = solveTuple.iterCount
        };

        return Task.FromResult(result);
    }
}
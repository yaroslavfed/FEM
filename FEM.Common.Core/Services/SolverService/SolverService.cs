using FEM.Common.DTO.Abstractions;
using FEM.Common.DTO.Models.MatrixFormats;
using FEM.Common.DTO.Models.OutputModels;
using FEM.Solvers.Solvers;

namespace FEM.Common.Core.Services.SolverService;

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
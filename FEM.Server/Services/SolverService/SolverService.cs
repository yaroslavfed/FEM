using FEM.Common.Data.MatrixFormats;
using FEM.Server.Data;
using FEM.Server.Data.OutputModels;
using FEM.Solvers.Solvers;

namespace FEM.Server.Services.SolverService;

// TODO: Реализовать резолвер для выбора решателя в зависимомти от формата хранения матрицы
public class SolverService : ISolverService
{
    // TODO: Это нужно переделать, пока просто костыль (возможно навсегда)
    public Task<SolutionResult> GetSolutionVectorAsync(IMatrixFormat matrixFormat, int maxIterationsCount, double eps)
    {
        if (matrixFormat is MatrixProfileFormat matrixProfile)
        {
            ISolver solver = new LosLUSolver(maxIterationsCount, eps);
            var solveTuple = solver.Solve(matrixProfile);

            var result = new SolutionResult
            {
                Solve = solveTuple.solve, SolutionInfo = null, ItersCount = solveTuple.iterCount
            };

            return Task.FromResult(result);
        }

        throw new NotImplementedException("Для данного типа матрицы решателя не реализовано");
    }
}
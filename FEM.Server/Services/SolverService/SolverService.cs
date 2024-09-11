using FEM.Common.Data.MathModels;
using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Solvers.Solvers;

namespace FEM.Server.Services.SolverService;

// TODO: Реализовать резолвер для выбора решателя в зависимомти от формата хранения матрицы
public class SolverService : ISolverService
{
    // TODO: Это нужно переделать, пока просто костыль (возможно навсегда)
    public Task<(Vector solve, double discrepancy, int iterCount)> GetSolutionVectorAsync(
        IMatrixFormat matrixFormat, int maxIterationsCount, double eps)
    {
        if (matrixFormat is MatrixProfileFormat matrixProfile)
        {
            ISolver solver = new LosLUSolver(maxIterationsCount, eps);
            var result = solver.Solve(matrixProfile);

            return Task.FromResult(result);
        }

        throw new NotImplementedException("Для данного типа матрицы решателя не реализовано");
    }
}
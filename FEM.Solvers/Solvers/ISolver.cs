using FEM.Common.Data.MathModels;
using FEM.Common.Data.MathModels.MatrixFormats;

namespace FEM.Solvers.Solvers;

public interface ISolver
{
    (Vector solve, double discrepancy, int iterCount) Solve(MatrixProfileFormat slae);
}
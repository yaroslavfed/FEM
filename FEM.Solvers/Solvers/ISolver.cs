using FEM.SharedDTO.Models.MathModels;
using FEM.SharedDTO.Models.MatrixFormats;

namespace FEM.Solvers.Solvers;

public interface ISolver
{
    (Vector solve, double discrepancy, int iterCount) Solve(MatrixProfileFormat slae);
}
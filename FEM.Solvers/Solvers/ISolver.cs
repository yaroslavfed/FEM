using FEM.Common.DTO.Models.MathModels;
using FEM.Common.DTO.Models.MatrixFormats;

namespace FEM.Solvers.Solvers;

public interface ISolver
{
    (Vector solve, double discrepancy, int iterCount) Solve(MatrixProfileFormat slae);
}
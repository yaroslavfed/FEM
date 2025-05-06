using FEM.Common.Data.MathModels;
using Vector = FEM.Server.Data.Domain.Vector;

namespace FEM.Server.Services.SolverService;

public interface ISolver
{
    Vector Solve(SparseMatrix matrix, Vector rhs);
}

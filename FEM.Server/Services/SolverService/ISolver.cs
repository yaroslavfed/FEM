using FEM.Server.Data.Domain;

namespace FEM.Server.Services.SolverService;

public interface ISolver
{
    Vector Solve(Matrix matrix, Vector rhs);
}

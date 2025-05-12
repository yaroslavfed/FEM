using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.BoundaryConditionService;

public interface IBoundaryConditionService
{
    Task ApplyBoundaryConditionsAsync(Matrix matrix, Vector rhs, Mesh mesh, double eps = 1e-8);
}
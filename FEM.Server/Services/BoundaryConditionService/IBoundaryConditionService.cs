using FEM.Common.Data.MathModels;
using Vector = FEM.Server.Data.Domain.Vector;

namespace FEM.Server.Services.BoundaryConditionService;

public interface IBoundaryConditionService
{
    public Task ApplyBoundaryConditionsAsync(SparseMatrix matrix, Vector rhs);
}
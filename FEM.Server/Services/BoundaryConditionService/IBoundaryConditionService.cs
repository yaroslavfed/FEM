using FEM.Server.Data.Domain;

namespace FEM.Server.Services.BoundaryConditionService;

public interface IBoundaryConditionService
{
    public Task ApplyBoundaryConditionsAsync(Matrix matrix, Vector rhs, IReadOnlyList<int> constrainedDofs);
}
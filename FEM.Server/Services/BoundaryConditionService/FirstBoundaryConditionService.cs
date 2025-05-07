using FEM.Server.Data.Domain;

namespace FEM.Server.Services.BoundaryConditionService;

public class FirstBoundaryConditionService : IBoundaryConditionService
{
    public Task ApplyBoundaryConditionsAsync(Matrix matrix, Vector rhs, IReadOnlyList<int> constrainedDofs)
    {
        foreach (int dof in constrainedDofs)
        {
            matrix.ClearRow(dof);
            matrix.ClearColumn(dof);
            matrix[dof, dof] = 1.0;
            rhs[dof] = 0.0;
        }

        return Task.CompletedTask;
    }
}
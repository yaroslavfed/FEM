using FEM.Server.Data.Domain;

namespace FEM.Server.Services.BoundaryConditionService;

public class FirstBoundaryConditionService : IBoundaryConditionService
{
    private readonly IBoundaryProvider _boundaryProvider;

    public FirstBoundaryConditionService(IBoundaryProvider boundaryProvider)
    {
        _boundaryProvider = boundaryProvider;
    }

    public async Task ApplyBoundaryConditionsAsync(SparseMatrix matrix, Vector rhs)
    {
        var constrainedDofs = await _boundaryProvider.GetConstrainedDegreesOfFreedomAsync();

        foreach (int dof in constrainedDofs)
        {
            matrix.ClearRow(dof);
            matrix.ClearColumn(dof);
            matrix[dof, dof] = 1.0;
            rhs[dof] = 0.0;
        }
    }
}
using FEM.Common.Enums;
using VectorFEM.Core.Services.Parallelepipedal.BoundaryConditionService.BoundaryConditions;

namespace VectorFEM.Core.Services.Parallelepipedal.BoundaryConditionService;

public class BoundaryConditionFactory
{
    public Task<IBoundaryConditionService> ResolveBoundaryCondition(EBoundaryConditions boundaryConditionType)
    {
        IBoundaryConditionService boundaryCondition = boundaryConditionType switch
        {
            EBoundaryConditions.Dirichlet => new FirstBoundaryConditionService(new TestingService.TestingService()),
            EBoundaryConditions.Neiman => throw new DllNotFoundException("Не реализовано"),
            EBoundaryConditions.Robin => throw new DllNotFoundException("Не реализовано"),
            _ => throw new ArgumentOutOfRangeException(nameof(boundaryConditionType), boundaryConditionType, null)
        };

        return Task.FromResult(boundaryCondition);
    }
}
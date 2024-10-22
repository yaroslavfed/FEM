using FEM.Common.Core.Services.BoundaryConditionService.BoundaryConditions;
using FEM.Common.Core.Services.ProblemService;
using FEM.Common.DTO.Enums;

namespace FEM.Common.Core.Services.BoundaryConditionService;

public class BoundaryConditionFactory : IBoundaryConditionFactory
{
    private readonly IProblemService _problemService;

    public BoundaryConditionFactory(IProblemService problemService)
    {
        _problemService = problemService;
    }

    public Task<IBoundaryConditionService> ResolveBoundaryConditionAsync(EBoundaryConditions boundaryConditionType)
    {
        IBoundaryConditionService boundaryCondition = boundaryConditionType switch
        {
            EBoundaryConditions.Dirichlet => new FirstBoundaryConditionService(_problemService),
            EBoundaryConditions.Neiman => throw new NotImplementedException("Не реализовано"),
            EBoundaryConditions.Robin => throw new NotImplementedException("Не реализовано"),
            _ => throw new ArgumentOutOfRangeException(nameof(boundaryConditionType), boundaryConditionType, null)
        };

        return Task.FromResult(boundaryCondition);
    }
}
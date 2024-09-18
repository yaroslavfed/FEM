using FEM.Common.Enums;
using FEM.Common.Extensions;
using FEM.Server.Services.Parallelepipedal.BoundaryConditionService.BoundaryConditions;
using FEM.Server.Services.TestingService;
using Splat;

namespace FEM.Server.Services.Parallelepipedal.BoundaryConditionService;

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
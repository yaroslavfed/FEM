using FEM.Common.Enums;
using FEM.Common.Extensions;
using Splat;
using VectorFEM.Core.Services.Parallelepipedal.BoundaryConditionService.BoundaryConditions;
using VectorFEM.Core.Services.TestingService;
using VectorFEM.Core.Services.TestSessionService;

namespace VectorFEM.Core.Services.Parallelepipedal.BoundaryConditionService;

public class BoundaryConditionFactory : IBoundaryConditionFactory
{
    private readonly ITestingService _testingService;

    public BoundaryConditionFactory(ITestingService testingService)
    {
        _testingService = testingService;
    }

    public Task<IBoundaryConditionService> ResolveBoundaryCondition(EBoundaryConditions boundaryConditionType)
    {
        IBoundaryConditionService boundaryCondition = boundaryConditionType switch
        {
            EBoundaryConditions.Dirichlet => Locator
                                             .Current
                                             .WithBuilder<FirstBoundaryConditionService>()
                                             .WithAutocomplete(_testingService)
                                             .BuildService(),
            EBoundaryConditions.Neiman => throw new DllNotFoundException("Не реализовано"),
            EBoundaryConditions.Robin => throw new DllNotFoundException("Не реализовано"),
            _ => throw new ArgumentOutOfRangeException(nameof(boundaryConditionType), boundaryConditionType, null)
        };

        return Task.FromResult(boundaryCondition);
    }
}
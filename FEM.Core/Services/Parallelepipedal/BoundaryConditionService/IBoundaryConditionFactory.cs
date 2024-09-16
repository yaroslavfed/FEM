using FEM.Common.Enums;
using FEM.Core.Services.Parallelepipedal.BoundaryConditionService.BoundaryConditions;

namespace FEM.Core.Services.Parallelepipedal.BoundaryConditionService;

public interface IBoundaryConditionFactory
{
    Task<IBoundaryConditionService> ResolveBoundaryConditionAsync(EBoundaryConditions boundaryConditionType);
}
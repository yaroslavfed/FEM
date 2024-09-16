using FEM.Common.Enums;
using FEM.Server.Services.Parallelepipedal.BoundaryConditionService.BoundaryConditions;

namespace FEM.Server.Services.Parallelepipedal.BoundaryConditionService;

public interface IBoundaryConditionFactory
{
    Task<IBoundaryConditionService> ResolveBoundaryConditionAsync(EBoundaryConditions boundaryConditionType);
}
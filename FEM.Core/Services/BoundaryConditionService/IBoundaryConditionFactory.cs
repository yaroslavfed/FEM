using FEM.Core.Services.BoundaryConditionService.BoundaryConditions;
using FEM.SharedDTO.Enums;

namespace FEM.Core.Services.BoundaryConditionService;

public interface IBoundaryConditionFactory
{
    Task<IBoundaryConditionService> ResolveBoundaryConditionAsync(EBoundaryConditions boundaryConditionType);
}
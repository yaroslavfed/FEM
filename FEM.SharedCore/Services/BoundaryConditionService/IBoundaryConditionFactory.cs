using FEM.SharedCore.Services.BoundaryConditionService.BoundaryConditions;
using FEM.SharedDTO.Enums;

namespace FEM.SharedCore.Services.BoundaryConditionService;

public interface IBoundaryConditionFactory
{
    Task<IBoundaryConditionService> ResolveBoundaryConditionAsync(EBoundaryConditions boundaryConditionType);
}
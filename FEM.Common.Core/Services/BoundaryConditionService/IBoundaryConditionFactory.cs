using FEM.Common.Core.Services.BoundaryConditionService.BoundaryConditions;
using FEM.Common.DTO.Enums;

namespace FEM.Common.Core.Services.BoundaryConditionService;

public interface IBoundaryConditionFactory
{
    Task<IBoundaryConditionService> ResolveBoundaryConditionAsync(EBoundaryConditions boundaryConditionType);
}
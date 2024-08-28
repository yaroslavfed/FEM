using FEM.Common.Enums;
using VectorFEM.Core.Services.Parallelepipedal.BoundaryConditionService.BoundaryConditions;

namespace VectorFEM.Core.Services.Parallelepipedal.BoundaryConditionService;

public interface IBoundaryConditionFactory
{
    Task<IBoundaryConditionService> ResolveBoundaryConditionAsync(EBoundaryConditions boundaryConditionType);
}
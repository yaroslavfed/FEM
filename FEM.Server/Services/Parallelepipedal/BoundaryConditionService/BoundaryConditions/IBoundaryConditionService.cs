using FEM.Common.Data.MeshModels;
using FEM.NonStationary.DTO.TestingContext;

namespace FEM.Server.Services.Parallelepipedal.BoundaryConditionService.BoundaryConditions;

public interface IBoundaryConditionService
{
    Task SetBoundaryConditionsAsync(NonStationaryTestSession<Mesh> nonStationaryTestSession, IMatrixFormat matrixProfile);
}
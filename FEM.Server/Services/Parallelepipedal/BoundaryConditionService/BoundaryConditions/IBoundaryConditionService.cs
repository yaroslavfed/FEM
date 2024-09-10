using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Common.Data.TestSession;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.Parallelepipedal.BoundaryConditionService.BoundaryConditions;

public interface IBoundaryConditionService
{
    Task SetBoundaryConditionsAsync(TestSession<Mesh> testSession, IMatrixFormat matrixProfile);
}
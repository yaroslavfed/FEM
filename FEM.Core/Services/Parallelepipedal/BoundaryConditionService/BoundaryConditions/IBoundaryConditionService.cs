using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Common.Data.TestSession;
using FEM.Core.Data.Parallelepipedal;

namespace FEM.Core.Services.Parallelepipedal.BoundaryConditionService.BoundaryConditions;

public interface IBoundaryConditionService
{
    Task SetBoundaryConditionsAsync(TestSession<Mesh> testSession, IMatrixFormat matrixProfile);
}
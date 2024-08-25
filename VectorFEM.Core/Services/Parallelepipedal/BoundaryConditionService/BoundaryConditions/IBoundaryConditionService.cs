using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Common.Data.TestSession;
using VectorFEM.Core.Data.Parallelepipedal;

namespace VectorFEM.Core.Services.Parallelepipedal.BoundaryConditionService.BoundaryConditions;

public interface IBoundaryConditionService
{
    Task<IMatrixFormat> AddBoundaryCondition(TestSession<Mesh> testSession);
}
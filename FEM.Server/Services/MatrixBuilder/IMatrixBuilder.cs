using FEM.Common.Data.MathModels;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.MatrixBuilder;

public interface IMatrixBuilder
{
    Task<Matrix> ComputeLocalStiffnessMatrixAsync(FiniteElement element);
}
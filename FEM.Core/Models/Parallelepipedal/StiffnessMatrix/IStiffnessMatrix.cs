using FEM.Core.Data.Parallelepipedal;

namespace FEM.Core.Models.Parallelepipedal.StiffnessMatrix;

public interface IStiffnessMatrix<TData>
{
    Task<TData> GetStiffnessMatrixAsync(double mu, FiniteElement finiteElement);
}
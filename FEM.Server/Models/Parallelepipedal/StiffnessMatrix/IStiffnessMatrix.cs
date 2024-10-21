using FEM.Common.Data.MeshModels;

namespace FEM.Server.Models.Parallelepipedal.StiffnessMatrix;

public interface IStiffnessMatrix<TData>
{
    Task<TData> GetStiffnessMatrixAsync(double mu, FiniteElement finiteElement);
}
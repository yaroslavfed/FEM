using FEM.SharedDTO.Models.MeshModels;

namespace FEM.SharedCore.Services.BaseMatrixServices.StiffnessMatrix;

public interface IStiffnessMatrix<TData>
{
    Task<TData> GetStiffnessMatrixAsync(double mu, FiniteElement finiteElement);
}
using FEM.Common.DTO.Models.MeshModels;

namespace FEM.Common.Core.Services.BaseMatrixServices.StiffnessMatrix;

public interface IStiffnessMatrix<TData>
{
    Task<TData> GetStiffnessMatrixAsync(double mu, FiniteElement finiteElement);
}
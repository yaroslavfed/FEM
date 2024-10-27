using FEM.SharedDTO.Models.MeshModels;

namespace FEM.Core.Services.BaseMatrixServices.MassMatrix;

public interface IMassMatrix<TData>
{
    IReadOnlyList<IReadOnlyList<double>> MassMatrixBase { get; }

    Task<TData> GetMassMatrixAsync(double gamma, FiniteElement finiteElement);
}
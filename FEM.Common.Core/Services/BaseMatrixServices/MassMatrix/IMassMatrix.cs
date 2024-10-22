using FEM.Common.DTO.Models.MeshModels;

namespace FEM.Common.Core.Services.BaseMatrixServices.MassMatrix;

public interface IMassMatrix<TData>
{
    IReadOnlyList<IReadOnlyList<double>> MassMatrixBase { get; }

    Task<TData> GetMassMatrixAsync(double gamma, FiniteElement finiteElement);
}
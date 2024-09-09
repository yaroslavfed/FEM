using FEM.Core.Data.Parallelepipedal;

namespace FEM.Core.Models.Parallelepipedal.MassMatrix;

public interface IMassMatrix<TData>
{
    IReadOnlyList<IReadOnlyList<double>> MassMatrixBase { get; }

    Task<TData> GetMassMatrixAsync(double gamma, FiniteElement finiteElement);
}
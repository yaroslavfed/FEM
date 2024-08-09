namespace VectorFEM.Core.Models.Parallelepipedal.MassMatrix;

public interface IMassMatrix<TData>
{
    IReadOnlyList<IReadOnlyList<double>> MassMatrixBase { get; }
    
    Task<TData> GetMassMatrixAsync(double gamma);
}
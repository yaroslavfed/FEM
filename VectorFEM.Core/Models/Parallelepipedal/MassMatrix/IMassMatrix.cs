namespace VectorFEM.Core.Models.Parallelepipedal.MassMatrix;

public interface IMassMatrix<out TData>
{
    IReadOnlyList<IReadOnlyList<double>> MassMatrixBase { get; }
    
    TData GetMassMatrix(double gamma);
}
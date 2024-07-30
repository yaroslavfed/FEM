namespace VectorFEM.Core.Models.Parallelepipedal.MassMatrix;

public interface IMassMatrix<out TData>
{
    TData GetMassMatrix(double gamma);
}
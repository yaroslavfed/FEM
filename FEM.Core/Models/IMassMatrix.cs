namespace FEM.Core.Models;

public interface IMassMatrix<out TData>
{
    TData GetMassMatrix(double gamma);
}
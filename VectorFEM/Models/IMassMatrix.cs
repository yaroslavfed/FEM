using VectorFEM.Data;

namespace VectorFEM.Models;

public interface IMassMatrix<out TData>
{
    TData GetMassMatrix(double gamma, Sensor? position = null);
}
using VectorFEM.Data;

namespace VectorFEM.Models;

public interface IMassMatrix<out TData>
{
    TData GetMassMatrix(FiniteElement element, Sensor? position = null);
}
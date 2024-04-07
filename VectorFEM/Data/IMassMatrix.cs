namespace VectorFEM.Data;

public interface IMassMatrix<out TData>
{
    TData GetMassMatrix(FiniteElement element, Sensor? position = null);
}
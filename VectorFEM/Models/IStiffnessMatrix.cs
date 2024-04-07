using VectorFEM.Data;

namespace VectorFEM.Models;

public interface IStiffnessMatrix<out TData>
{
    TData GetStiffnessMatrix(double mu, FiniteElement element, Sensor? position = null);
}
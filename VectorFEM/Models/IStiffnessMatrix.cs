using VectorFEM.Data;

namespace VectorFEM.Models;

public interface IStiffnessMatrix<out TData>
{
    TData GetStiffnessMatrix(double mu, Sensor? position = null);
}
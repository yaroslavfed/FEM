using VectorFEM.Data;

namespace VectorFEM.Core.Models;

public interface IStiffnessMatrix<out TData>
{
    TData GetStiffnessMatrix(double mu, Sensor? position = null);
}
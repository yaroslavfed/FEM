namespace FEM.Core.Models;

public interface IStiffnessMatrix<out TData>
{
    TData GetStiffnessMatrix(double mu);
}
namespace VectorFEM.Core.Models.Parallelepipedal.StiffnessMatrix;

public interface IStiffnessMatrix<out TData>
{
    TData GetStiffnessMatrix(double mu);
}
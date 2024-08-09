namespace VectorFEM.Core.Models.Parallelepipedal.StiffnessMatrix;

public interface IStiffnessMatrix<TData>
{
    Task<TData> GetStiffnessMatrixAsync(double mu);
}
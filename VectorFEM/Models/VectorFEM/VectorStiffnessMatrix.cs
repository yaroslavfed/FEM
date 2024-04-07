using VectorFEM.Data;
using VectorFEM.Services.StiffnessMatrixResolver;

namespace VectorFEM.Models.VectorFEM;

public class VectorStiffnessMatrix(FiniteElement element) : IStiffnessMatrix<Matrix>
{
    private readonly Lazy<double[][]> _stiffnessMatrix1 = new(() =>
    [
        [2, 1, -2, -1],
        [1, 2, -1, -2],
        [-2, -1, 2, 1],
        [-1, -2, 1, 2],
    ]);

    private readonly Lazy<double[][]> _stiffnessMatrix2 = new(() =>
    [
        [2, -2, 1, -1],
        [-2, 2, -1, 1],
        [1, -1, 2, -2],
        [-1, 1, -2, 2],
    ]);

    private readonly Lazy<double[][]> _stiffnessMatrix3 = new(() =>
    [
        [-2, 2, -1, 1],
        [-1, 1, -2, 2],
        [2, -2, 1, -1],
        [1, -1, 2, -2],
    ]);

    public Matrix GetStiffnessMatrix(double mu, FiniteElement element, Sensor? position = null)
    {
        var matrix = new Matrix
        {
            Data = 
        };

        matrix *= 1 / mu;

        return matrix;
    }
}
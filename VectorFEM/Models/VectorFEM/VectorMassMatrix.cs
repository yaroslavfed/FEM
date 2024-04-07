using VectorFEM.Data;
using VectorFEM.Services.BasicFunctionResolver;

namespace VectorFEM.Models.VectorFEM;

public class VectorMassMatrix(FiniteElement element) : IMassMatrix<Matrix>
{
    private readonly Lazy<double[][]> massMatrix = new(() =>
        [
            [4, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0],
            [2, 4, 1, 2, 0, 0, 0, 0, 0, 0, 0, 0],
            [2, 1, 4, 2, 0, 0, 0, 0, 0, 0, 0, 0],
            [1, 2, 2, 4, 0, 0, 0, 0, 0, 0, 0, 0],
            [0, 0, 0, 0, 4, 2, 2, 1, 0, 0, 0, 0],
            [0, 0, 0, 0, 2, 4, 1, 2, 0, 0, 0, 0],
            [0, 0, 0, 0, 2, 1, 4, 2, 0, 0, 0, 0],
            [0, 0, 0, 0, 1, 2, 2, 4, 0, 0, 0, 0],
            [0, 0, 0, 0, 0, 0, 0, 0, 4, 2, 2, 1],
            [0, 0, 0, 0, 0, 0, 0, 0, 2, 4, 1, 2],
            [0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 4, 2],
            [0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 4],
        ]
    );

    public Matrix GetMassMatrix(double gamma, Sensor? position = null)
    {
        var matrix = new Matrix
        {
            Data = massMatrix.Value
        };
        matrix *= gamma
                  * (element.Xn - element.X0)
                  * (element.Yn - element.Y0)
                  * (element.Zn - element.Z0)
                  / 36;

        return matrix;
    }
}
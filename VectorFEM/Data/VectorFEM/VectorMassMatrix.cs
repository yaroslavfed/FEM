using VectorFEM.Services.BasicFunctionResolver;

namespace VectorFEM.Data.VectorFEM;

public class VectorMassMatrix(IBasicFunctionsResolver<Vector> basicFunctionsResolver) : IMassMatrix<Matrix>
{
    public Matrix GetMassMatrix(FiniteElement element, Sensor? position = null)
    {
        var matrix = new Matrix
        {
            Data =
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
        };
        matrix *= (element.Xn - element.X0)
            * (element.Yn - element.Y0)
            * (element.Zn - element.Z0) / 36;

        return matrix;
    }
}
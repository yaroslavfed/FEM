using VectorFEM.Core.Extensions;
using VectorFEM.Data;

namespace VectorFEM.Core.Models.VectorFEM;

internal class StiffnessVectorMatrix(FiniteElement element) : IStiffnessMatrix<Matrix>
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

    public Matrix GetStiffnessMatrix(double mu, Sensor? position = null)
    {
        var m111 = new Matrix { Data = _stiffnessMatrix1.Value };
        m111 *= (element.Xn - element.X0) * (element.Yn - element.Y0) / 6 * (element.Zn - element.Z0);
        var m112 = new Matrix { Data = _stiffnessMatrix2.Value };
        m112 *= (element.Xn - element.X0) * (element.Zn - element.Z0) / 6 * (element.Yn - element.Y0);
        var m11 = m111 + m112;

        var m221 = new Matrix { Data = _stiffnessMatrix1.Value };
        m221 *= (element.Xn - element.X0) * (element.Yn - element.Y0) / 6 * (element.Zn - element.Z0);
        var m222 = new Matrix { Data = _stiffnessMatrix2.Value };
        m222 *= (element.Yn - element.Y0) * (element.Zn - element.Z0) / 6 * (element.Xn - element.X0);
        var m22 = m221 + m222;

        var m331 = new Matrix { Data = _stiffnessMatrix1.Value };
        m331 *= (element.Xn - element.X0) * (element.Zn - element.Z0) / 6 * (element.Yn - element.Y0);
        var m332 = new Matrix { Data = _stiffnessMatrix2.Value };
        m332 *= (element.Yn - element.Y0) * (element.Zn - element.Z0) / 6 * (element.Xn - element.X0);
        var m33 = m331 + m332;

        var m121 = new Matrix { Data = _stiffnessMatrix2.Value };
        m121 *= (element.Zn - element.Z0) / -6;
        var m12 = m121;

        var m131 = new Matrix { Data = _stiffnessMatrix3.Value };
        m131 *= (element.Yn - element.Y0) / 6;
        var m13 = m131;

        var m31 = m13.Transpose();

        var m231 = new Matrix { Data = _stiffnessMatrix1.Value };
        m231 *= (element.Xn - element.X0) / -6;
        var m23 = m231;

        var array = new double[12, 12];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                array[i, j] = m11.Data[i][j];
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                array[i + 4, j + 4] = m22.Data[i][j];
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                array[i + 8, j + 8] = m33.Data[i][j];
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                array[i, j + 4] = m12.Data[i][j];
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                array[i, j + 8] = m13.Data[i][j];
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                array[i + 4, j] = m12.Data[i][j];
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                array[i + 4, j + 8] = m23.Data[i][j];
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                array[i + 8, j] = m31.Data[i][j];
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                array[i + 8, j + 4] = m23.Data[i][j];
            }
        }

        var matrix = new Matrix
        {
            Data = array.ArrayToList()
        };

        matrix *= 1 / mu;

        return matrix;
    }
}
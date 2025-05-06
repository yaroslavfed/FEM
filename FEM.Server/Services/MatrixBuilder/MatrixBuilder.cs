using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Extensions;

namespace FEM.Server.Services.MatrixBuilder;

public class MatrixBuilder : IMatrixBuilder
{
    public async Task<Matrix> ComputeLocalStiffnessMatrixAsync(FiniteElement element)
    {
        const int edgeCount = 12; // 12 ребер у гексаэдра (brick element)
        var matrix = new double[edgeCount, edgeCount];
        var mu = element.Mu;

        // Определим точки для интегрирования — кубатура (здесь: одна точка по центру, можно заменить на более точную схему)
        var integrationPoints = new[]
        {
            new Sensor(0.0, 0.0, 0.0)
        };
        var weights = new[]
        {
            8.0
        }; // вес центра куба на [-1,1]^3

        for (int i = 0; i < edgeCount; i++)
        {
            var curlWi = await _basisFunctionService.GetBasisFunctionCurlAsync(element, i);

            for (int j = 0; j < edgeCount; j++)
            {
                var curlWj = await _basisFunctionService.GetBasisFunctionCurlAsync(element, j);

                double integral = 0;

                for (int k = 0; k < integrationPoints.Length; k++)
                {
                    var point = integrationPoints[k];
                    var weight = weights[k];

                    // Значения роторов в точке
                    var valueCurlWi = curlWi(point);
                    var valueCurlWj = curlWj(point);

                    // (1/μ) * curlWi · curlWj
                    var dot = (1.0 / mu)
                              * (valueCurlWi.X * valueCurlWj.X
                                 + valueCurlWi.Y * valueCurlWj.Y
                                 + valueCurlWi.Z * valueCurlWj.Z);

                    // Добавим в интеграл с учетом веса и масштаба
                    var volume = element.GetSizes().X * element.GetSizes().Y * element.GetSizes().Z;
                    integral += dot * weight * volume / 8.0; // делим на 8, т.к. координаты в [-1,1]
                }

                matrix[i, j] = integral;
            }
        }

        return new Matrix(matrix);
    }

}
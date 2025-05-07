using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Extensions;
using FEM.Server.Services.BasisFunctionProvider;

namespace FEM.Server.Services.MatrixBuilder;

public class MatrixBuilder : IMatrixBuilder
{
    private readonly IBasisFunctionProvider _basisFunctionProvider;

    public MatrixBuilder(IBasisFunctionProvider basisFunctionProvider)
    {
        _basisFunctionProvider = basisFunctionProvider;
    }

    /// <inheritdoc />
    public Task<Matrix> ComputeLocalStiffnessMatrixAsync(FiniteElement element)
    {
        const int edgeCount = 12;
        var localMatrix = new Matrix(edgeCount, edgeCount);

        var volume = element.GetSizes().X * element.GetSizes().Y * element.GetSizes().Z;
        var center = element.GetCenter();

        for (int i = 0; i < edgeCount; i++)
        {
            var curlI = _basisFunctionProvider.GetCurl(element, i, center);

            for (int j = 0; j < edgeCount; j++)
            {
                var curlJ = _basisFunctionProvider.GetCurl(element, j, center);

                var dot = curlI.Dot(curlJ);
                localMatrix[i, j] = (1.0 / element.Mu) * dot * volume;
            }
        }

        return Task.FromResult(localMatrix);
    }
}
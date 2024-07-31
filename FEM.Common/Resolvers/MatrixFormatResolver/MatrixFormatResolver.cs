using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Common.Enums;

namespace FEM.Common.Resolvers.MatrixFormatResolver;

public class MatrixFormatResolver : IMatrixFormatResolver
{
    public IMatrixFormat ResolveMatrixFormat(EMatrixFormats matrixFormat)
    {
        return matrixFormat switch
        {
            EMatrixFormats.Profile => new MatrixProfileFormat(),
            _                      => throw new ArgumentOutOfRangeException(nameof(matrixFormat), matrixFormat, null)
        };
    }
}
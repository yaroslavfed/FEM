using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Common.Enums;

namespace FEM.Common.Resolvers.MatrixFormatResolver;

public interface IMatrixFormatResolver
{
    IMatrixFormat ResolveMatrixFormat(EMatrixFormats matrixFormat);
}
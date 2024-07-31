using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Common.Enums;

namespace VectorFEM.Core.Services.Parallelepipedal.GlobalMatrixService;

public interface IGlobalMatrixServices
{
    Task<IMatrixFormat> GetGlobalMatrixAsync(EMatrixFormats matrixFormat);
}
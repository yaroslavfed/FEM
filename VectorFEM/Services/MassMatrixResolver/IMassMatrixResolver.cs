using VectorFEM.Enums;
using VectorFEM.Models;

namespace VectorFEM.Services.MassMatrixResolver;

public interface IMassMatrixResolver<out TData>
{
    IMassMatrix<TData> ResolveMassMatrixStrategy(EFemType femType);
}
using VectorFEM.Data;

namespace VectorFEM.Services.MassMatrixResolver;

public interface IMassMatrixResolver<out TData>
{
    IMassMatrix<TData> ResolveMassMatrixStrategy(EFemType femType);
}
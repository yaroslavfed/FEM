using VectorFEM.Core.Enums;
using VectorFEM.Core.Models;
using VectorFEM.Data;

namespace VectorFEM.Core.Services.Resolvers.MassMatrixResolver;

public interface IMassMatrixResolver<out TData>
{
    IMassMatrix<TData> ResolveMassMatrixStrategy(FiniteElement element, EFemType femType);
}
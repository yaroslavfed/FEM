using VectorFEM.Core.Enums;
using VectorFEM.Core.Models;
using VectorFEM.Core.Models.VectorFEM;
using VectorFEM.Data;

namespace VectorFEM.Core.Services.Resolvers.MassMatrixResolver;

internal class MassMatrixResolver<TData> : IMassMatrixResolver<TData>
{
    public IMassMatrix<TData> ResolveMassMatrixStrategy(FiniteElement element, EFemType femType) =>
        femType switch
        {
            EFemType.Vector => (IMassMatrix<TData>)new MassVectorMatrix(element),
            _ => throw new TypeAccessException()
        };
}
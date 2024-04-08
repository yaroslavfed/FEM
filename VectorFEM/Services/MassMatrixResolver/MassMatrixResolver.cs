using VectorFEM.Data;
using VectorFEM.Enums;
using VectorFEM.Models;
using VectorFEM.Models.VectorFEM;

namespace VectorFEM.Services.MassMatrixResolver;

public class MassMatrixResolver<TData> : IMassMatrixResolver<TData>
{
    public IMassMatrix<TData> ResolveMassMatrixStrategy(FiniteElement element, EFemType femType) =>
        femType switch
        {
            EFemType.Vector => (IMassMatrix<TData>)new MassVectorMatrix(element),
            _ => throw new TypeAccessException()
        };
}
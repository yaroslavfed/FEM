using VectorFEM.Data;
using VectorFEM.Enums;
using VectorFEM.Models;
using VectorFEM.Models.VectorFEM;

namespace VectorFEM.Services.MassMatrixResolver;

public class MassMatrixResolver<TData>(FiniteElement element) : IMassMatrixResolver<TData>
{
    public IMassMatrix<TData> ResolveMassMatrixStrategy(EFemType femType) =>
        femType switch
        {
            EFemType.Vector => (IMassMatrix<TData>)new VectorMassMatrix(element),
            _ => throw new TypeAccessException()
        };
}
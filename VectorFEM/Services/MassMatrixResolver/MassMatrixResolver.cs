using VectorFEM.Data;
using VectorFEM.Data.VectorFEM;
using VectorFEM.Services.BasicFunctionResolver;

namespace VectorFEM.Services.MassMatrixResolver;

public class MassMatrixResolver<TData> : IMassMatrixResolver<TData>
{
    public IMassMatrix<TData> ResolveMassMatrixStrategy(EFemType femType)
    {
        return femType switch
        {
            EFemType.Vector => (IMassMatrix<TData>)new VectorMassMatrix(new BasicFunctionsResolver<Vector>()),
            _ => throw new TypeAccessException()
        };
    }
}
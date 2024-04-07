using VectorFEM.Data;
using VectorFEM.Data.VectorFEM;

namespace VectorFEM.Services.BasicFunctionResolver;

public class BasicFunctionsResolver<TData> : IBasicFunctionsResolver<TData>
{
    public IBasicFunction<TData> ResolveBasicFunctionStrategy(EFemType femType)
    {
        return femType switch
        {
            EFemType.Vector => (IBasicFunction<TData>)new BasicVectorFunction(),
            _ => throw new TypeAccessException()
        };
    }
}
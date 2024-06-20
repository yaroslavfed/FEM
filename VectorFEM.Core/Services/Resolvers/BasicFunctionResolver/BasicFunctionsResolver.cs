using VectorFEM.Core.Enums;
using VectorFEM.Core.Models;
using VectorFEM.Core.Models.VectorFEM;

namespace VectorFEM.Core.Services.Resolvers.BasicFunctionResolver;

internal class BasicFunctionsResolver<TData> : IBasicFunctionsResolver<TData>
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
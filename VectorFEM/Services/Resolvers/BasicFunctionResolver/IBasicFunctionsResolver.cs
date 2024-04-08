using VectorFEM.Enums;
using VectorFEM.Models;

namespace VectorFEM.Services.Resolvers.BasicFunctionResolver;

public interface IBasicFunctionsResolver<out TData>
{
    IBasicFunction<TData> ResolveBasicFunctionStrategy(EFemType femType);
}
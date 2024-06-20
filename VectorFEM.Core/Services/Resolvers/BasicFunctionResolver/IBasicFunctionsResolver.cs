using VectorFEM.Core.Enums;
using VectorFEM.Core.Models;

namespace VectorFEM.Core.Services.Resolvers.BasicFunctionResolver;

public interface IBasicFunctionsResolver<out TData>
{
    IBasicFunction<TData> ResolveBasicFunctionStrategy(EFemType femType);
}
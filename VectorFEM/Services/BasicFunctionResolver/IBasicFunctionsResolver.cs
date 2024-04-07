using VectorFEM.Data;

namespace VectorFEM.Services.BasicFunctionResolver;

public interface IBasicFunctionsResolver<out TData>
{
    IBasicFunction<TData> ResolveBasicFunctionStrategy(EFemType femType);
}
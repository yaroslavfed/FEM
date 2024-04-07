using VectorFEM.Enums;
using VectorFEM.Models;

namespace VectorFEM.Services.BasicFunctionResolver;

public interface IBasicFunctionsResolver<out TData>
{
    IBasicFunction<TData> ResolveBasicFunctionStrategy(EFemType femType);
}
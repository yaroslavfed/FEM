using VectorFEM.Core.Enums;
using VectorFEM.Core.Models;
using VectorFEM.Shared.Domain;

namespace VectorFEM.Core.Services.MatrixServices.Resolvers.BasicFunctionResolver;

public interface IBasicFunctionsResolver<out TData>
{
    IBasicFunction<TData> ResolveBasicFunctionStrategy(FiniteElement element, EFemType femType);
}
using FEM.Common.Data.Domain;
using FEM.Core.Enums;
using FEM.Core.Models;

namespace FEM.Core.Services.MatrixServices.Resolvers.BasicFunctionResolver;

public interface IBasicFunctionsResolver<out TData>
{
    IBasicFunction<TData> ResolveBasicFunctionStrategy(FiniteElement element, EFemType femType);
}
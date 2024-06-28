using FEM.Core.Enums;
using FEM.Core.Models;
using FEM.Shared.Domain;
using FEM.Shared.Domain.Data;

namespace FEM.Core.Services.MatrixServices.Resolvers.BasicFunctionResolver;

public interface IBasicFunctionsResolver<out TData>
{
    IBasicFunction<TData> ResolveBasicFunctionStrategy(FiniteElement element, EFemType femType);
}
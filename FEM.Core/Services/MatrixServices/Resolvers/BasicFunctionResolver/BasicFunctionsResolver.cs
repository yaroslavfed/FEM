using FEM.Common.Data.Domain;
using FEM.Core.Enums;
using FEM.Core.Extensions;
using FEM.Core.Models;
using FEM.Core.Models.VectorFEM;
using Splat;

namespace FEM.Core.Services.MatrixServices.Resolvers.BasicFunctionResolver;

internal class BasicFunctionsResolver<TData> : IBasicFunctionsResolver<TData>
{
    public IBasicFunction<TData> ResolveBasicFunctionStrategy(FiniteElement element, EFemType femType)
    {
        return femType switch
        {
            EFemType.Vector => (IBasicFunction<TData>)
                Locator.Current
                    .WithBuilder<BasicVectorFunction>()
                    .WithAutocomplete(element)
                    .BuildService(),
            _ => throw new TypeAccessException()
        };
    }
}
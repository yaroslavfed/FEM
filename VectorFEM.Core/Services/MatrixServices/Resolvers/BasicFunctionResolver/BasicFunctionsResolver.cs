using Splat;
using VectorFEM.Core.Enums;
using VectorFEM.Core.Extensions;
using VectorFEM.Core.Models;
using VectorFEM.Core.Models.VectorFEM;
using VectorFEM.Shared.Domain;

namespace VectorFEM.Core.Services.MatrixServices.Resolvers.BasicFunctionResolver;

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
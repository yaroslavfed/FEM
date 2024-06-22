using Splat;
using VectorFEM.Core.Enums;
using VectorFEM.Core.Extensions;
using VectorFEM.Core.Models;
using VectorFEM.Core.Models.VectorFEM;
using VectorFEM.Shared.Domain;

namespace VectorFEM.Core.Services.Resolvers.MassMatrixResolver;

internal class MassMatrixResolver<TData> : IMassMatrixResolver<TData>
{
    public IMassMatrix<TData> ResolveMassMatrixStrategy(FiniteElement element, EFemType femType) =>
        femType switch
        {
            EFemType.Vector => (IMassMatrix<TData>)
                Locator.Current
                    .WithBuilder<MassVectorMatrix>()
                    .WithAutocomplete(element)
                    .BuildService(),
            _ => throw new TypeAccessException()
        };
}
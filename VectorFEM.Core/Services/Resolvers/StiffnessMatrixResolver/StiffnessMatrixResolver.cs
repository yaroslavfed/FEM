using Splat;
using VectorFEM.Core.Enums;
using VectorFEM.Core.Extensions;
using VectorFEM.Core.Models;
using VectorFEM.Core.Models.VectorFEM;
using VectorFEM.Shared.Domain;

namespace VectorFEM.Core.Services.Resolvers.StiffnessMatrixResolver;

internal class StiffnessMatrixResolver<TData> : IStiffnessMatrixResolver<TData>
{
    public IStiffnessMatrix<TData> ResolveStiffnessMatrixStrategy(FiniteElement element, EFemType femType) =>
        femType switch
        {
            EFemType.Vector => (IStiffnessMatrix<TData>)
                Locator.Current
                    .WithBuilder<StiffnessVectorMatrix>()
                    .WithAutocomplete(element)
                    .BuildService(),
            _ => throw new TypeAccessException()
        };
}
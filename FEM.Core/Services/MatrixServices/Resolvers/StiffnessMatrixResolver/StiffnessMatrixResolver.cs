using FEM.Core.Enums;
using FEM.Core.Extensions;
using FEM.Core.Models;
using FEM.Core.Models.VectorFEM;
using FEM.Shared.Domain;
using FEM.Shared.Domain.Data;
using Splat;

namespace FEM.Core.Services.MatrixServices.Resolvers.StiffnessMatrixResolver;

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
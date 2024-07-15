using FEM.Common.Data.Domain;
using FEM.Core.Enums;
using FEM.Core.Extensions;
using FEM.Core.Models;
using FEM.Core.Models.VectorFEM;
using FEM.Shared.Domain.Data;
using Splat;

namespace FEM.Core.Services.MatrixServices.Resolvers.MassMatrixResolver;

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
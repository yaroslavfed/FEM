using VectorFEM.Core.Enums;
using VectorFEM.Core.Models;
using VectorFEM.Core.Models.VectorFEM;
using VectorFEM.Data;

namespace VectorFEM.Core.Services.Resolvers.StiffnessMatrixResolver;

internal class StiffnessMatrixResolver<TData> : IStiffnessMatrixResolver<TData>
{
    public IStiffnessMatrix<TData> ResolveStiffnessMatrixStrategy(FiniteElement element, EFemType femType) =>
        femType switch
        {
            EFemType.Vector => (IStiffnessMatrix<TData>)new StiffnessVectorMatrix(element),
            _ => throw new TypeAccessException()
        };
}
using VectorFEM.Data;
using VectorFEM.Enums;
using VectorFEM.Models;
using VectorFEM.Models.VectorFEM;

namespace VectorFEM.Services.StiffnessMatrixResolver;

public class StiffnessMatrixResolver<TData> : IStiffnessMatrixResolver<TData>
{
    public IStiffnessMatrix<TData> ResolveStiffnessMatrixStrategy(FiniteElement element, EFemType femType) =>
        femType switch
        {
            EFemType.Vector => (IStiffnessMatrix<TData>)new StiffnessVectorMatrix(element),
            _ => throw new TypeAccessException()
        };
}
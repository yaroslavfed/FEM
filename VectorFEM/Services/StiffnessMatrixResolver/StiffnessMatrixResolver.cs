using VectorFEM.Data;
using VectorFEM.Enums;
using VectorFEM.Models;
using VectorFEM.Models.VectorFEM;

namespace VectorFEM.Services.StiffnessMatrixResolver;

public class StiffnessMatrixResolver<TData>(FiniteElement element) : IStiffnessMatrixResolver<TData>
{
    public IStiffnessMatrix<TData> ResolveStiffnessMatrixStrategy(EFemType femType) =>
        femType switch
        {
            EFemType.Vector => (IStiffnessMatrix<TData>)new VectorStiffnessMatrix(element),
            _ => throw new TypeAccessException()
        };
}
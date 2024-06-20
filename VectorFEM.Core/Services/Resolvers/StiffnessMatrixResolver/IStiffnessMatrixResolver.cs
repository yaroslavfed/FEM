using VectorFEM.Core.Enums;
using VectorFEM.Core.Models;
using VectorFEM.Data;

namespace VectorFEM.Core.Services.Resolvers.StiffnessMatrixResolver;

public interface IStiffnessMatrixResolver<out TData>
{
    IStiffnessMatrix<TData> ResolveStiffnessMatrixStrategy(FiniteElement element, EFemType femType);
}
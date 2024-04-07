using VectorFEM.Data;
using VectorFEM.Enums;
using VectorFEM.Models;

namespace VectorFEM.Services.StiffnessMatrixResolver;

public interface IStiffnessMatrixResolver<out TData>
{
    IStiffnessMatrix<TData> ResolveStiffnessMatrixStrategy(FiniteElement element, EFemType femType);
}
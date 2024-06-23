using VectorFEM.Core.Enums;
using VectorFEM.Core.Models;
using VectorFEM.Shared.Domain;

namespace VectorFEM.Core.Services.MatrixServices.Resolvers.StiffnessMatrixResolver;

public interface IStiffnessMatrixResolver<out TData>
{
    IStiffnessMatrix<TData> ResolveStiffnessMatrixStrategy(FiniteElement element, EFemType femType);
}
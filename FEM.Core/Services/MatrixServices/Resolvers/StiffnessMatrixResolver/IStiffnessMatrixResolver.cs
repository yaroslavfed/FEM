using FEM.Core.Enums;
using FEM.Core.Models;
using FEM.Shared.Domain;

namespace FEM.Core.Services.MatrixServices.Resolvers.StiffnessMatrixResolver;

public interface IStiffnessMatrixResolver<out TData>
{
    IStiffnessMatrix<TData> ResolveStiffnessMatrixStrategy(FiniteElement element, EFemType femType);
}
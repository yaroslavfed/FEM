using FEM.Core.Enums;
using FEM.Core.Models;
using FEM.Shared.Domain;

namespace FEM.Core.Services.MatrixServices.Resolvers.MassMatrixResolver;

public interface IMassMatrixResolver<out TData>
{
    IMassMatrix<TData> ResolveMassMatrixStrategy(FiniteElement element, EFemType femType);
}
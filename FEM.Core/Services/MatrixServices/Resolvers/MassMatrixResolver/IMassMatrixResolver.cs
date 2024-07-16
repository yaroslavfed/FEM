using FEM.Common.Data.Domain;
using FEM.Core.Enums;
using FEM.Core.Models;

namespace FEM.Core.Services.MatrixServices.Resolvers.MassMatrixResolver;

public interface IMassMatrixResolver<out TData>
{
    IMassMatrix<TData> ResolveMassMatrixStrategy(FiniteElement element, EFemType femType);
}
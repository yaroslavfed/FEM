using FEM.Core.Enums;
using FEM.Core.Services.MatrixServices.Resolvers.MassMatrixResolver;
using FEM.Core.Services.MatrixServices.Resolvers.StiffnessMatrixResolver;
using FEM.Shared.Domain;
using FEM.Shared.Domain.MathModels;

namespace FEM.Core.Services.MatrixServices.GlobalMatrixServices;

internal class GlobalMatrixServices(
    IMassMatrixResolver<Matrix> massMatrixResolver,
    IStiffnessMatrixResolver<Matrix> stiffnessMatrixResolver
) : IGlobalMatrixServices
{
    public Matrix GetGlobalMatrix(double mu, double gamma, FiniteElement element, EFemType femType)
    {
        var massMatrix = massMatrixResolver.ResolveMassMatrixStrategy(element, femType)
            .GetMassMatrix(gamma);
        var stiffnessMatrix = stiffnessMatrixResolver.ResolveStiffnessMatrixStrategy(element, femType)
            .GetStiffnessMatrix(mu);

        // TODO: реализовать сбор глобальной матрицы в соответствии с профилем

        return massMatrix + stiffnessMatrix;
    }
}
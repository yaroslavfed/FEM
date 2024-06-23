using VectorFEM.Core.Enums;
using VectorFEM.Core.Services.MatrixServices.Resolvers.MassMatrixResolver;
using VectorFEM.Core.Services.MatrixServices.Resolvers.StiffnessMatrixResolver;
using VectorFEM.Shared.Domain;
using VectorFEM.Shared.Domain.MathModels;

namespace VectorFEM.Core.Services.MatrixServices.GlobalMatrixServices;

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
using VectorFEM.Core.Enums;
using VectorFEM.Core.Services.Resolvers.MassMatrixResolver;
using VectorFEM.Core.Services.Resolvers.StiffnessMatrixResolver;
using VectorFEM.Data;

namespace VectorFEM.Core.Services;

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
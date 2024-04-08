using VectorFEM.Data;
using VectorFEM.Enums;
using VectorFEM.Services.Resolvers.MassMatrixResolver;
using VectorFEM.Services.Resolvers.StiffnessMatrixResolver;

namespace VectorFEM.Services;

internal class GlobalMatrixServices(
    IMassMatrixResolver<Matrix> massMatrixResolver,
    IStiffnessMatrixResolver<Matrix> stiffnessMatrixResolver) : IGlobalMatrixServices
{
    public Matrix GetGlobalMatrix(double mu, double gamma, FiniteElement element, EFemType femType)
    {
        var massMatrix = massMatrixResolver.ResolveMassMatrixStrategy(element, femType).GetMassMatrix(gamma);
        var stiffnessMatrix = stiffnessMatrixResolver.ResolveStiffnessMatrixStrategy(element, femType)
            .GetStiffnessMatrix(mu);

        // TODO: реализовать сбор глобальной матрицы в соответствии с профилем

        return massMatrix + stiffnessMatrix;
    }
}
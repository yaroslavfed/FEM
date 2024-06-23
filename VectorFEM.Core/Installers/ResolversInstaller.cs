using Autofac;
using VectorFEM.Core.Services.MatrixServices.Resolvers.BasicFunctionResolver;
using VectorFEM.Core.Services.MatrixServices.Resolvers.MassMatrixResolver;
using VectorFEM.Core.Services.MatrixServices.Resolvers.StiffnessMatrixResolver;
using VectorFEM.Shared.Domain.MathModels;

namespace VectorFEM.Core.Installers;

public static class ResolversInstaller
{
    public static void RegisterResolvers(this ContainerBuilder builder)
    {
        builder.RegisterType<BasicFunctionsResolver<Vector>>().As<IBasicFunctionsResolver<Vector>>();
        builder.RegisterType<MassMatrixResolver<Matrix>>().As<IMassMatrixResolver<Matrix>>();
        builder.RegisterType<StiffnessMatrixResolver<Matrix>>().As<IStiffnessMatrixResolver<Matrix>>();
    }
}
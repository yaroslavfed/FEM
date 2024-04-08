using Autofac;
using VectorFEM.Data;
using VectorFEM.Services.Resolvers.BasicFunctionResolver;
using VectorFEM.Services.Resolvers.MassMatrixResolver;
using VectorFEM.Services.Resolvers.StiffnessMatrixResolver;

namespace VectorFEM.Installers;

public static class ResolversInstaller
{
    public static void RegisterResolvers(this ContainerBuilder builder)
    {
        builder.RegisterType<BasicFunctionsResolver<Vector>>().As<IBasicFunctionsResolver<Vector>>();
        builder.RegisterType<MassMatrixResolver<Matrix>>().As<IMassMatrixResolver<Matrix>>();
        builder.RegisterType<StiffnessMatrixResolver<Matrix>>().As<IStiffnessMatrixResolver<Matrix>>();
    }
}
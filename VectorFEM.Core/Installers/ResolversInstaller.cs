using Autofac;
using VectorFEM.Core.Services.Resolvers.BasicFunctionResolver;
using VectorFEM.Core.Services.Resolvers.MassMatrixResolver;
using VectorFEM.Core.Services.Resolvers.StiffnessMatrixResolver;
using VectorFEM.Data;

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
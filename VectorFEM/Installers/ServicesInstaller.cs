using Autofac;
using VectorFEM.Data;
using VectorFEM.Services.BasicFunctionResolver;
using VectorFEM.Services.MassMatrixResolver;
using VectorFEM.Services.StiffnessMatrixResolver;

namespace VectorFEM.Installers;

public static class ServicesInstaller
{
    public static void RegisterServices(this ContainerBuilder builder)
    {
        builder.RegisterResolvers();
    }

    private static void RegisterResolvers(this ContainerBuilder builder)
    {
        builder.RegisterType<BasicFunctionsResolver<Vector>>().As<IBasicFunctionsResolver<Vector>>();
        builder.RegisterType<MassMatrixResolver<Matrix>>().As<IMassMatrixResolver<Matrix>>();
        builder.RegisterType<StiffnessMatrixResolver>().As<IStiffnessMatrixResolver>();
    }
}
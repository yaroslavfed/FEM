using Autofac;
using FEM.Common.Data.MathModels;
using FEM.Core.Services.MatrixServices.Resolvers.BasicFunctionResolver;
using FEM.Core.Services.MatrixServices.Resolvers.MassMatrixResolver;
using FEM.Core.Services.MatrixServices.Resolvers.StiffnessMatrixResolver;

namespace FEM.Core.Installers;

public static class ResolversInstaller
{
    public static void RegisterResolvers(this ContainerBuilder builder)
    {
        builder.RegisterType<BasicFunctionsResolver<Vector>>().As<IBasicFunctionsResolver<Vector>>();
        builder.RegisterType<MassMatrixResolver<Matrix>>().As<IMassMatrixResolver<Matrix>>();
        builder.RegisterType<StiffnessMatrixResolver<Matrix>>().As<IStiffnessMatrixResolver<Matrix>>();
    }
}
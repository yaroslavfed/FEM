using Autofac;
using VectorFEM.Core.Services;

namespace VectorFEM.Core.Installers;

public static class ServicesInstaller
{
    public static void RegisterServices(this ContainerBuilder builder)
    {
        builder.RegisterType<GlobalMatrixServices>().As<IGlobalMatrixServices>();
    }
}
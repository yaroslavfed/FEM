using Autofac;
using VectorFEM.Core.Services;
using VectorFEM.Resources.RichDomainObjects;

namespace VectorFEM.Core.Installers;

public static class ServicesInstaller
{
    public static void RegisterServices(this ContainerBuilder builder)
    {
        builder.RegisterType<GlobalMatrixServices>().As<IGlobalMatrixServices>();
        builder.RegisterType<Mesh>();
    }
}
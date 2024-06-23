using Autofac;
using FEM.Common.RichDomainObjects;
using FEM.Core.Services.MatrixServices.GlobalMatrixServices;

namespace FEM.Core.Installers;

public static class ServicesInstaller
{
    public static void RegisterServices(this ContainerBuilder builder)
    {
        builder.RegisterType<GlobalMatrixServices>().As<IGlobalMatrixServices>();
        builder.RegisterType<Mesh>();
    }
}
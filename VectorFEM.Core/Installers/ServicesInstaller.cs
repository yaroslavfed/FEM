using Autofac;
using VectorFEM.Core.Services;
using VectorFEM.Core.Services.MatrixServices.GlobalMatrixServices;
using VectorFEM.GridBuilder.RichDomainObjects;

namespace VectorFEM.Core.Installers;

public static class ServicesInstaller
{
    public static void RegisterServices(this ContainerBuilder builder)
    {
        builder.RegisterType<GlobalMatrixServices>().As<IGlobalMatrixServices>();
        builder.RegisterType<Mesh>();
    }
}
using Autofac;
using FEM.Core.Services.MatrixServices.GlobalMatrixServices;
using FEM.Core.Services.MeshService;

namespace FEM.Core.Installers;

public static class ServicesInstaller
{
    public static void RegisterServices(this ContainerBuilder builder)
    {
        builder.RegisterType<GlobalMatrixServices>().As<IGlobalMatrixServices>();
        builder.RegisterType<MeshService>().As<IMeshService>();

        Storage.Installers.ServicesInstaller.RegisterServices(builder);
    }
}
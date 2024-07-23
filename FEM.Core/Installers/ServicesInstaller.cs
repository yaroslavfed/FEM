using Autofac;
using FEM.Core.Services.MatrixServices.GlobalMatrixServices;
using FEM.Core.Services.MeshService;
using FEM.Core.Services.NumberingService.EdgesNumberingService;
using FEM.Core.Services.NumberingService.NodesNumberingService;

namespace FEM.Core.Installers;

public static class ServicesInstaller
{
    public static void RegisterServices(this ContainerBuilder builder)
    {
        builder.RegisterType<GlobalMatrixServices>().As<IGlobalMatrixServices>();
        builder.RegisterType<MeshService>().As<IMeshService>();
        builder.RegisterType<NodesNumberingService>().As<INodesNumberingService>();
        builder.RegisterType<EdgesNumberingService>().As<IEdgesNumberingService>();

        Storage.Installers.ServicesInstaller.RegisterServices(builder);
    }
}
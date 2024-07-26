using Autofac;
using FEM.Common.Data.Domain;
using FEM.Core.Data.Dto.MatrixFormat;
using FEM.Core.Services.DrawingService;
using FEM.Core.Services.DrawingService.MeshDrawing;
using FEM.Core.Services.MatrixServices.GlobalMatrixServices;
using FEM.Core.Services.MatrixServices.GlobalMatrixServices.VectorGlobalMatrixService;
using FEM.Core.Services.MatrixServices.MatrixPortraitService;
using FEM.Core.Services.MeshService;
using FEM.Core.Services.NumberingService.EdgesNumberingService;
using FEM.Core.Services.NumberingService.NodesNumberingService;

namespace FEM.Core.Installers;

public static class ServicesInstaller
{
    public static void RegisterServices(this ContainerBuilder builder)
    {
        builder.RegisterType<VectorGlobalMatrixService>().As<IGlobalMatrixServices>();
        builder.RegisterType<MeshService>().As<IMeshService>();
        builder.RegisterType<NodesNumberingService>().As<INodesNumberingService>();
        builder.RegisterType<EdgesNumberingService>().As<IEdgesNumberingService>();
        builder.RegisterType<MeshDrawingService<Mesh>>().As<IDrawingService<Mesh>>().SingleInstance();
        builder
            .RegisterType<VectorMatrixPortraitService<Mesh, MatrixProfileFormat>>()
            .As<IMatrixPortraitService<Mesh, MatrixProfileFormat>>()
            .SingleInstance();

        Storage.Installers.ServicesInstaller.RegisterServices(builder);
    }
}
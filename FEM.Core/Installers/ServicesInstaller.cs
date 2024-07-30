using Autofac;
using FEM.Common.Data.MathModels.MatrixFormats;
using VectorFEM.Core.Services.Parallelepipedal.DrawingMeshService;
using VectorFEM.Core.Services.Parallelepipedal.GlobalMatrixService;
using VectorFEM.Core.Services.Parallelepipedal.MatrixPortraitService;
using VectorFEM.Core.Services.Parallelepipedal.MeshService;
using VectorFEM.Core.Services.Parallelepipedal.NumberingService.EdgesNumberingService;
using VectorFEM.Core.Services.Parallelepipedal.NumberingService.NodesNumberingService;

namespace FEM.Core.Installers;

public static class ServicesInstaller
{
    public static void RegisterServices(this ContainerBuilder builder)
    {
        builder
            .RegisterType<ProfileGlobalMatrixService<MatrixProfileFormat>>()
            .As<IGlobalMatrixServices<MatrixProfileFormat>>();
        builder.RegisterType<MeshService>().As<IMeshService>();
        builder.RegisterType<NodesNumberingService>().As<INodesNumberingService>();
        builder.RegisterType<EdgesNumberingService>().As<IEdgesNumberingService>();
        builder.RegisterType<MeshDrawingService>().As<IMeshDrawingService>().SingleInstance();
        builder
            .RegisterType<MatrixPortraitService<MatrixProfileFormat>>()
            .As<IMatrixPortraitService<MatrixProfileFormat>>()
            .SingleInstance();

        Storage.Installers.ServicesInstaller.RegisterServices(builder);
    }
}
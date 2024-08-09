﻿using Autofac;
using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Common.Resolvers.MatrixFormatResolver;
using VectorFEM.Core.Services.Parallelepipedal.DrawingMeshService;
using VectorFEM.Core.Services.Parallelepipedal.GlobalMatrixService;
using VectorFEM.Core.Services.Parallelepipedal.MatrixPortraitService;
using VectorFEM.Core.Services.Parallelepipedal.MeshService;
using VectorFEM.Core.Services.Parallelepipedal.NumberingService.EdgesNumberingService;
using VectorFEM.Core.Services.Parallelepipedal.NumberingService.NodesNumberingService;
using VectorFEM.Core.Services.Parallelepipedal.RightPartVectorService;

namespace FEM.Core.Installers;

public static class ServicesInstaller
{
    public static void RegisterServices(this ContainerBuilder builder)
    {
        builder.RegisterType<GlobalMatrixService>().As<IGlobalMatrixServices>();
        builder.RegisterType<RightPartVectorService>().As<IRightPartVectorService>();
        builder.RegisterType<MeshService>().As<IMeshService>();
        builder.RegisterType<NodesNumberingService>().As<INodesNumberingService>();
        builder.RegisterType<EdgesNumberingService>().As<IEdgesNumberingService>();
        builder.RegisterType<MeshDrawingService>().As<IMeshDrawingService>().SingleInstance();
        builder.RegisterType<MatrixPortraitService>().As<IMatrixPortraitService>().SingleInstance();

        builder.RegisterType<MatrixFormatResolver>().As<IMatrixFormatResolver>().SingleInstance();

        Storage.Installers.ServicesInstaller.RegisterServices(builder);
    }
}
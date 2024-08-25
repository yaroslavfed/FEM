using Autofac;
using FEM.Common.Data.MathModels;
using FEM.Common.Resolvers.MatrixFormatResolver;
using VectorFEM.Core.Models.Parallelepipedal.MassMatrix;
using VectorFEM.Core.Models.Parallelepipedal.StiffnessMatrix;
using VectorFEM.Core.Services.Parallelepipedal.DrawingMeshService;
using VectorFEM.Core.Services.Parallelepipedal.GlobalMatrixService;
using VectorFEM.Core.Services.Parallelepipedal.MatrixPortraitService;
using VectorFEM.Core.Services.Parallelepipedal.MeshService;
using VectorFEM.Core.Services.Parallelepipedal.NumberingService.EdgesNumberingService;
using VectorFEM.Core.Services.Parallelepipedal.NumberingService.NodesNumberingService;
using VectorFEM.Core.Services.Parallelepipedal.RightPartVectorService;
using VectorFEM.Core.Services.TestingService;
using VectorFEM.Core.Services.TestSessionService;

namespace FEM.Core.Installers;

public static class ServicesInstaller
{
    public static void RegisterServices(this ContainerBuilder builder)
    {
        builder.RegisterType<GlobalMatrixService>().As<IGlobalMatrixServices>();
        builder.RegisterType<RightPartVectorService>().As<IRightPartVectorService>();

        builder.RegisterType<StiffnessMatrix>().As<IStiffnessMatrix<Matrix>>();
        builder.RegisterType<MassMatrix>().As<IMassMatrix<Matrix>>();

        builder.RegisterType<MeshService>().As<IMeshService>();
        builder.RegisterType<MatrixPortraitService>().As<IMatrixPortraitService>();

        builder.RegisterType<NodesNumberingService>().As<INodesNumberingService>();
        builder.RegisterType<EdgesNumberingService>().As<IEdgesNumberingService>();

        builder.RegisterType<TestingService>().As<ITestingService>();
        builder.RegisterType<TestSessionService>().As<ITestSessionService>();

        builder.RegisterType<MatrixFormatResolver>().As<IMatrixFormatResolver>();
        builder.RegisterType<VisualizerService>().As<IVisualizerService>().SingleInstance();

        Storage.Installers.ServicesInstaller.RegisterStorageServices(builder);
    }
}
using Autofac;
using FEM.Common.Data.MathModels;
using FEM.Common.Resolvers.MatrixFormatResolver;
using FEM.Server.Models.Parallelepipedal.MassMatrix;
using FEM.Server.Models.Parallelepipedal.StiffnessMatrix;
using FEM.Server.Services.Parallelepipedal.BoundaryConditionService;
using FEM.Server.Services.Parallelepipedal.DrawingMeshService;
using FEM.Server.Services.Parallelepipedal.GlobalMatrixService;
using FEM.Server.Services.Parallelepipedal.MatrixPortraitService;
using FEM.Server.Services.Parallelepipedal.MeshService;
using FEM.Server.Services.Parallelepipedal.NumberingService.EdgesNumberingService;
using FEM.Server.Services.Parallelepipedal.NumberingService.NodesNumberingService;
using FEM.Server.Services.Parallelepipedal.RightPartVectorService;
using FEM.Server.Services.TestingService;
using FEM.Server.Services.TestSessionService;
using FEM.Storage.Converter;

namespace FEM.Server.Installers;

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
        builder.RegisterType<BoundaryConditionFactory>().As<IBoundaryConditionFactory>();
        
        builder.RegisterType<VisualizerService>().As<IVisualizerService>();
        builder.RegisterType<ConverterService>().As<IConverterService>();
        
    }
}
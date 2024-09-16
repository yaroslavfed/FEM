using Autofac;
using FEM.Common.Data.MathModels;
using FEM.Common.Resolvers.MatrixFormatResolver;
using FEM.Core.Models.Parallelepipedal.MassMatrix;
using FEM.Core.Models.Parallelepipedal.StiffnessMatrix;
using FEM.Core.Services.Parallelepipedal.BoundaryConditionService;
using FEM.Core.Services.Parallelepipedal.DrawingMeshService;
using FEM.Core.Services.Parallelepipedal.GlobalMatrixService;
using FEM.Core.Services.Parallelepipedal.MatrixPortraitService;
using FEM.Core.Services.Parallelepipedal.MeshService;
using FEM.Core.Services.Parallelepipedal.NumberingService.EdgesNumberingService;
using FEM.Core.Services.Parallelepipedal.NumberingService.NodesNumberingService;
using FEM.Core.Services.Parallelepipedal.RightPartVectorService;
using FEM.Core.Services.TestingService;
using FEM.Core.Services.TestSessionService;
using FEM.Storage.Converter;

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
        builder.RegisterType<BoundaryConditionFactory>().As<IBoundaryConditionFactory>();
        
        builder.RegisterType<VisualizerService>().As<IVisualizerService>();
        builder.RegisterType<ConverterService>().As<IConverterService>();
        
    }
}
using FEM.Common.Data.MathModels;
using FEM.Common.Resolvers.MatrixFormatResolver;
using FEM.Server.Models.BasicFunction;
using FEM.Server.Models.Parallelepipedal.MassMatrix;
using FEM.Server.Models.Parallelepipedal.StiffnessMatrix;
using FEM.Server.Services.AssemblyService;
using FEM.Server.Services.BoundaryConditionService;
using FEM.Server.Services.GlobalMatrixService;
using FEM.Server.Services.InaccuracyService;
using FEM.Server.Services.MatrixPortraitService;
using FEM.Server.Services.MeshService;
using FEM.Server.Services.NumberingService.EdgesNumberingService;
using FEM.Server.Services.NumberingService.NodesNumberingService;
using FEM.Server.Services.PlotService;
using FEM.Server.Services.ProblemService;
using FEM.Server.Services.RightPartVectorService;
using FEM.Server.Services.SaverService;
using FEM.Server.Services.SolverService;
using FEM.Server.Services.SourceProvider;
using FEM.Server.Services.TestResultService;
using FEM.Server.Services.TestSessionService;
using FEM.Server.Services.VisualizerService;
using FEM.Storage.Converter;

namespace FEM.Server.Installers;

public static class ServicesInstaller
{
    public static void AddServices(this IServiceCollection builder)
    {
        builder.AddScoped<IGlobalMatrixServices, GlobalMatrixService>();
        builder.AddScoped<IRightPartVectorService, RightPartVectorService>();
        builder.AddScoped<ISolverService, SolverService>();

        builder.AddScoped<IStiffnessMatrix<Matrix>, StiffnessMatrix>();
        builder.AddScoped<IMassMatrix<Matrix>, MassMatrix>();

        builder.AddScoped<IMeshService, MeshService>();
        builder.AddScoped<IMatrixPortraitService, MatrixPortraitService>();

        builder.AddScoped<INodesNumberingService, NodesNumberingService>();
        builder.AddScoped<IEdgesNumberingService, EdgesNumberingService>();

        builder.AddScoped<IProblemService, ProblemService>();
        builder.AddScoped<ITestSessionService, TestSessionService>();

        builder.AddScoped<IMatrixFormatResolver, MatrixFormatResolver>();
        builder.AddScoped<IBoundaryConditionFactory, BoundaryConditionFactory>();

        builder.AddScoped<IPlotService, PlotService>();
        builder.AddScoped<IVisualizerService, VisualizerService>();
        builder.AddScoped<IConverterService, ConverterService>();

        builder.AddScoped<ITestResultService, TestResultService>();
        builder.AddScoped<ISaverService, SaverService>();

        builder.AddScoped<IInaccuracyService, InaccuracyService>();

        builder.AddScoped<IBasicFunction, BasicFunction>();
        builder.AddScoped<ICurrentSourceProvider, CurrentSourceProvider>();
        builder.AddScoped<IAssemblyService, AssemblyService>();
    }
}
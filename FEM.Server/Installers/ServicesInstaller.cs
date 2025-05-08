using FEM.Common.Resolvers.MatrixFormatResolver;
using FEM.Server.Models.BasicFunction;
using FEM.Server.Services.AssemblyService;
using FEM.Server.Services.BasisFunctionProvider;
using FEM.Server.Services.BoundaryConditionService;
using FEM.Server.Services.MatrixPortraitService;
using FEM.Server.Services.MeshService;
using FEM.Server.Services.NumberingService.EdgesNumberingService;
using FEM.Server.Services.NumberingService.NodesNumberingService;
using FEM.Server.Services.PlotService;
using FEM.Server.Services.ProblemService;
using FEM.Server.Services.SaverService;
using FEM.Server.Services.SolutionExportService;
using FEM.Server.Services.SourceProvider;
using FEM.Server.Services.TestResultService;
using FEM.Server.Services.TestSessionService;
using FEM.Server.Services.VisualizerService;

namespace FEM.Server.Installers;

public static class ServicesInstaller
{
    public static void AddServices(this IServiceCollection builder)
    {
        builder.AddScoped<IMeshService, MeshService>();
        builder.AddScoped<IMatrixPortraitService, MatrixPortraitService>();
        builder.AddScoped<IMatrixFormatResolver, MatrixFormatResolver>();
        
        builder.AddScoped<INodesNumberingService, NodesNumberingService>();
        builder.AddScoped<IEdgesNumberingService, EdgesNumberingService>();

        builder.AddScoped<IProblemService, ProblemService>();
        builder.AddScoped<IBasisFunctionProvider, BasicFunctionProvider>();
        builder.AddScoped<ITestSessionService, TestSessionService>();
        builder.AddScoped<IBoundaryConditionService, FirstBoundaryConditionService>();

        builder.AddScoped<IPlotService, PlotService>();
        builder.AddScoped<IVisualizerService, VisualizerService>();
        builder.AddScoped<ISolutionExportService, SolutionExportService>();

        builder.AddScoped<ITestResultService, TestResultService>();
        builder.AddScoped<ISaverService, SaverService>();

        builder.AddScoped<IBasicFunction, BasicFunction>();
        builder.AddScoped<ICurrentSourceProvider, CurrentSourceProvider>();
        builder.AddScoped<IAssemblyService, AssemblyService>();
    }
}
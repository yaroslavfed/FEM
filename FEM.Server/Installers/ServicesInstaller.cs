using FEM.Common.Core.Services.BaseMatrixServices.MassMatrix;
using FEM.Common.Core.Services.BaseMatrixServices.StiffnessMatrix;
using FEM.Common.Core.Services.BoundaryConditionService;
using FEM.Common.Core.Services.GlobalMatrixService;
using FEM.Common.Core.Services.InaccuracyService;
using FEM.Common.Core.Services.MatrixPortraitService;
using FEM.Common.Core.Services.ProblemService;
using FEM.Common.Core.Services.RightPartVectorService;
using FEM.Common.Core.Services.SolverService;
using FEM.Common.Core.Services.TestResultService;
using FEM.Common.Core.Services.TestSessionService;
using FEM.Common.Data.MathModels;
using FEM.Common.DTO.Models.MathModels;
using FEM.Common.Resolvers.MatrixFormatResolver;
using FEM.Common.Services.MeshService;
using FEM.Common.Services.NumberingService.EdgesNumberingService;
using FEM.Common.Services.NumberingService.NodesNumberingService;
using FEM.Common.Services.SaverService;
using FEM.Common.Services.VisualizerService;
using FEM.Server.Services.ProblemService;
using FEM.Stationary.Core.Services.TestSessionService;
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

        builder.AddScoped<IBoundaryConditionFactory, BoundaryConditionFactory>();

        builder.AddScoped<IVisualizerService, VisualizerService>();
        builder.AddScoped<IConverterService, ConverterService>();

        builder.AddScoped<ITestResultService, TestResultService>();
        builder.AddScoped<ISaverService, SaverService>();

        builder.AddScoped<IInaccuracyService, InaccuracyService>();
    }
}
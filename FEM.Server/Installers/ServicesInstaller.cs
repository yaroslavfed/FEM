using FEM.Core.Services.BaseMatrixServices.MassMatrix;
using FEM.Core.Services.BaseMatrixServices.StiffnessMatrix;
using FEM.Core.Services.BoundaryConditionService;
using FEM.Core.Services.InaccuracyService;
using FEM.Core.Services.MatrixPortraitService;
using FEM.Core.Services.MeshService;
using FEM.Core.Services.NumberingService.EdgesNumberingService;
using FEM.Core.Services.NumberingService.NodesNumberingService;
using FEM.Core.Services.ProblemService;
using FEM.Core.Services.SolverService;
using FEM.SharedDTO.Models.MathModels;
using FEM.Storage.Converter;
using Stationary.Core.Services.GlobalMatrixService;
using Stationary.Core.Services.RightPartVectorService;
using Stationary.Core.Services.SaverService;
using Stationary.Core.Services.TestResultService;
using Stationary.Core.Services.TestSessionService;
using Stationary.Core.Services.VisualizerService;

namespace FEM.Server.Installers;

public static class ServicesInstaller
{
    public static void AddServices(this IServiceCollection builder)
    {
        builder.AddScoped<ISolverService, SolverService>();
        builder.AddScoped<IStiffnessMatrix<Matrix>, StiffnessMatrix>();
        builder.AddScoped<IMassMatrix<Matrix>, MassMatrix>();
        builder.AddScoped<IMeshService, MeshService>();
        builder.AddScoped<IMatrixPortraitService, MatrixPortraitService>();
        builder.AddScoped<INodesNumberingService, NodesNumberingService>();
        builder.AddScoped<IEdgesNumberingService, EdgesNumberingService>();
        builder.AddScoped<IBoundaryConditionFactory, BoundaryConditionFactory>();
        builder.AddScoped<IStationaryVisualizerService, StationaryVisualizerService>();
        builder.AddScoped<IConverterService, ConverterService>();
        builder.AddScoped<IStationarySaverService, StationarySaverService>();
        builder.AddScoped<IInaccuracyService, InaccuracyService>();
        builder.AddScoped<IStationaryTestSessionService, StationaryTestSessionService>();
        builder.AddScoped<IStationaryGlobalMatrixServices, StationaryGlobalMatrixService>();
        builder.AddScoped<IStationaryRightPartVectorService, StationaryRightPartVectorService>();
        builder.AddScoped<IProblemService, ProblemService>();
        builder.AddScoped<IStationaryTestResultService, StationaryTestResultService>();
    }
}
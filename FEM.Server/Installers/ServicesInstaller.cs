using FEM.SharedCore.Services.BaseMatrixServices.MassMatrix;
using FEM.SharedCore.Services.BaseMatrixServices.StiffnessMatrix;
using FEM.SharedCore.Services.BoundaryConditionService;
using FEM.SharedCore.Services.InaccuracyService;
using FEM.SharedCore.Services.MatrixPortraitService;
using FEM.SharedCore.Services.MeshService;
using FEM.SharedCore.Services.NumberingService.EdgesNumberingService;
using FEM.SharedCore.Services.NumberingService.NodesNumberingService;
using FEM.SharedCore.Services.ProblemService;
using FEM.SharedCore.Services.SolverService;
using FEM.SharedCore.Services.TestSessionService;
using FEM.SharedDTO.Models.MathModels;
using FEM.Storage.Converter;
using NonStationary.Core.Services.TestSessionService;
using NonStationary.Core.Services.TimeService;
using NonStationary.DTO.Configurations;
using NonStationary.DTO.TestingContext;
using Stationary.Core.Services.GlobalMatrixService;
using Stationary.Core.Services.RightPartVectorService;
using Stationary.Core.Services.SaverService;
using Stationary.Core.Services.TestResultService;
using Stationary.Core.Services.TestSessionService;
using Stationary.Core.Services.VisualizerService;
using Stationary.DTO.Configurations;
using Stationary.DTO.TestingContext;

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
        builder
            .AddScoped<ITestSessionService<StationaryTestSession, StationaryTestConfiguration>,
                StationaryTestSessionService>();
        builder
            .AddScoped<ITestSessionService<NonStationaryTestSession, NonStationaryTestConfiguration>,
                NonStationaryTestSessionService>();
        builder.AddScoped<ITimeService, TimeService>();
        builder.AddScoped<IStationaryGlobalMatrixServices, StationaryGlobalMatrixService>();
        builder.AddScoped<IStationaryRightPartVectorService, StationaryRightPartVectorService>();
        builder.AddScoped<IProblemService, ProblemService>();
        builder.AddScoped<IStationaryTestResultService, StationaryTestResultService>();
    }
}
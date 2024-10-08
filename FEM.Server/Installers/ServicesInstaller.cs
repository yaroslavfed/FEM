﻿using FEM.Common.Data.MathModels;
using FEM.Common.Resolvers.MatrixFormatResolver;
using FEM.Server.Models.Parallelepipedal.MassMatrix;
using FEM.Server.Models.Parallelepipedal.StiffnessMatrix;
using FEM.Server.Services.InaccuracyService;
using FEM.Server.Services.Parallelepipedal.BoundaryConditionService;
using FEM.Server.Services.Parallelepipedal.GlobalMatrixService;
using FEM.Server.Services.Parallelepipedal.MatrixPortraitService;
using FEM.Server.Services.Parallelepipedal.MeshService;
using FEM.Server.Services.Parallelepipedal.NumberingService.EdgesNumberingService;
using FEM.Server.Services.Parallelepipedal.NumberingService.NodesNumberingService;
using FEM.Server.Services.Parallelepipedal.RightPartVectorService;
using FEM.Server.Services.Parallelepipedal.VisualizerService;
using FEM.Server.Services.ProblemService;
using FEM.Server.Services.SaverService;
using FEM.Server.Services.SolverService;
using FEM.Server.Services.TestingService;
using FEM.Server.Services.TestResultService;
using FEM.Server.Services.TestSessionService;
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

        builder.AddScoped<IVisualizerService, VisualizerService>();
        builder.AddScoped<IConverterService, ConverterService>();

        builder.AddScoped<ITestResultService, TestResultService>();
        builder.AddScoped<ISaverService, SaverService>();

        builder.AddScoped<IInaccuracyService, InaccuracyService>();
    }
}
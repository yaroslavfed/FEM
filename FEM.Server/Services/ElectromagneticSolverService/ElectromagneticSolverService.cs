using FEM.Common.Data.MathModels;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Models.CurrentSource;
using FEM.Server.Services.AssemblyService;
using FEM.Server.Services.BoundaryConditionService;
using Vector = FEM.Server.Data.Domain.Vector;

namespace FEM.Server.Services.SolverService;

public class ElectromagneticSolverService : IElectromagneticSolverService
{
    private readonly IAssemblyService          _assemblyService;
    private readonly IBoundaryConditionService _boundaryConditionService;
    private readonly ISolver                   _solver;

    public ElectromagneticSolverService(
        IAssemblyService matrixAssemblyService,
        ISolver solver,
        IBoundaryConditionService boundaryConditionService
    )
    {
        _assemblyService = matrixAssemblyService;
        _solver = solver;
        _boundaryConditionService = boundaryConditionService;
    }

    public async Task<Vector> SolveAsync(Mesh mesh, IReadOnlyList<ICurrentSource> sources)
    {
        // 1. Сборка матрицы жесткости
        var globalSystem = await _assemblyService.AssembleGlobalSystemAsync(mesh, sources);

        // 3. Граничные условия
        await _boundaryConditionService.ApplyBoundaryConditionsAsync(globalSystem.globalMatrix, globalSystem.globalRhs);

        // 4. Решение СЛАУ
        var solution = _solver.Solve(globalSystem.globalMatrix, globalSystem.globalRhs);

        return solution;
    }
}
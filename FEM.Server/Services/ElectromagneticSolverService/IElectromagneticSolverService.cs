using FEM.Common.Data.MathModels;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Models.CurrentSource;
using Vector = FEM.Server.Data.Domain.Vector;

namespace FEM.Server.Services.SolverService;

public interface IElectromagneticSolverService
{
    Task<Vector> SolveAsync(Mesh mesh, IReadOnlyList<ICurrentSource> sources);
}
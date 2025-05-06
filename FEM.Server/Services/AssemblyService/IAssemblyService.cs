using FEM.Common.Data.MathModels;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Models.CurrentSource;
using Vector = FEM.Server.Data.Domain.Vector;

namespace FEM.Server.Services.AssemblyService;

public interface IAssemblyService
{
    Task<(SparseMatrix globalMatrix, Vector globalRhs)> AssembleGlobalSystemAsync(
        Mesh mesh,
        IReadOnlyList<ICurrentSource> sources
    );
}
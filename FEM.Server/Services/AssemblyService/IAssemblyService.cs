using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.AssemblyService;

public interface IAssemblyService
{
    Task<(Matrix GlobalMatrix, Vector GlobalRhs)> AssembleGlobalSystemAsync(
        Mesh mesh,
        IReadOnlyList<CurrentSegment> sources
    );
}
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.MatrixAssemblyService;

public interface IMatrixAssemblyService
{
    Task<SparseMatrix> AssembleStiffnessMatrixAsync(Mesh mesh);
}

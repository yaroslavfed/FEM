using FEM.Common.Data.MathModels;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Models.CurrentSource;
using FEM.Server.Services.MatrixBuilder;
using FEM.Server.Services.ProblemService;
using FEM.Server.Services.VectorBuilder;
using Vector = FEM.Server.Data.Domain.Vector;

namespace FEM.Server.Services.AssemblyService;

public class AssemblyService : IAssemblyService
{
    private readonly IProblemService _problemService;
    private readonly IMatrixBuilder  _matrixBuilder;
    private readonly IVectorBuilder  _vectorBuilder;

    public AssemblyService(IProblemService problemService, IMatrixBuilder matrixBuilder, IVectorBuilder vectorBuilder)
    {
        _problemService = problemService;
        _matrixBuilder = matrixBuilder;
        _vectorBuilder = vectorBuilder;
    }

    public async Task<(SparseMatrix globalMatrix, Vector globalRhs)> AssembleGlobalSystemAsync(
        Mesh mesh,
        IReadOnlyList<ICurrentSource> sources
    )
    {
        var globalMatrix = new SparseMatrix();
        var globalVector = new Dictionary<int, double>();

        foreach (var element in mesh.Elements)
        {
            var localMatrix = await _matrixBuilder.ComputeLocalStiffnessMatrixAsync(element);
            var localVector = await _vectorBuilder.ComputeLocalRightHandSideAsync(element, sources);

            var localEdgeNumbers = _connectivityService.GetLocalToGlobalEdgeMap(element);

            for (int i = 0; i < 12; i++)
            {
                int globalI = localEdgeNumbers[i];

                if (!globalVector.ContainsKey(globalI))
                    globalVector[globalI] = 0;
                globalVector[globalI] += localVector[i];

                for (int j = 0; j < 12; j++)
                {
                    int globalJ = localEdgeNumbers[j];
                    globalMatrix.Add(globalI, globalJ, localMatrix[i, j]);
                }
            }
        }

        var rhs = Vector.FromDictionary(globalVector, globalMatrix.Size);
        return (globalMatrix, rhs);
    }
}
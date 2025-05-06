using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.MatrixAssemblyService;

public class MatrixAssemblyService : IMatrixAssemblyService
{
    private readonly IBasisFunctionProvider   _basisFunctionProvider;

    public MatrixAssemblyService(
        IBasisFunctionProvider basisFunctionProvider,
        IMaterialPropertyService materialService)
    {
        _basisFunctionProvider = basisFunctionProvider;
        _materialService = materialService;
    }

    public async Task<SparseMatrix> AssembleStiffnessMatrixAsync(Mesh mesh)
    {
        var matrix = new SparseMatrix(mesh.Edges.Count);

        foreach (var element in mesh.Elements)
        {
            var localMatrix = new double[12, 12];

            for (int i = 0; i < 12; i++)
            {
                var curl_i = _basisFunctionProvider.GetCurl(element, i);

                for (int j = 0; j < 12; j++)
                {
                    var curl_j = _basisFunctionProvider.GetCurl(element, j);

                    var dot = curl_i.DotProduct(curl_j);
                    var volume = element.Volume;

                    localMatrix[i, j] += (1.0 / element.Mu) * dot * volume;
                }
            }

            // Глобальная сборка
            for (int i = 0; i < 12; i++)
            {
                int global_i = mesh.GetGlobalEdgeIndex(element, i);
                for (int j = 0; j < 12; j++)
                {
                    int global_j = mesh.GetGlobalEdgeIndex(element, j);
                    matrix[global_i, global_j] += localMatrix[i, j];
                }
            }
        }

        return matrix;
    }
}

using FEM.Common.Data.MathModels;
using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Common.Data.TestSession;
using FEM.Core.Data.Parallelepipedal;
using FEM.Core.Models.Parallelepipedal.MassMatrix;
using FEM.Core.Models.Parallelepipedal.StiffnessMatrix;

namespace FEM.Core.Services.Parallelepipedal.GlobalMatrixService;

public class GlobalMatrixService : IGlobalMatrixServices
{
    private readonly IStiffnessMatrix<Matrix> _stiffnessMatrix;
    private readonly IMassMatrix<Matrix>      _massMatrix;

    public GlobalMatrixService(IStiffnessMatrix<Matrix> stiffnessMatrix, IMassMatrix<Matrix> massMatrix)
    {
        _stiffnessMatrix = stiffnessMatrix;
        _massMatrix = massMatrix;
    }

    /// <inheritdoc cref="IGlobalMatrixServices.GetGlobalMatrixAsync"/>
    public async Task GetGlobalMatrixAsync(IMatrixFormat matrixProfile, TestSession<Mesh> testSession)
    {
        foreach (var element in testSession.Mesh.Elements)
        {
            await ResolveGlobalMatrixAsync(matrixProfile, element, testSession);
        }
    }

    /// <summary>
    /// Собираем глобальную матрицу в заданном формате хранения
    /// </summary>
    /// <param name="matrixProfile">Выбранный формат хранения</param>
    /// <param name="element">Выбранный КЭ</param>
    /// <param name="testSession"><see cref="TestSession{TMesh}"/></param>
    private async Task ResolveGlobalMatrixAsync(
        IMatrixFormat matrixProfile,
        FiniteElement element,
        TestSession<Mesh> testSession
    )
    {
        var massMatrix = await _massMatrix.GetMassMatrixAsync(testSession.Gamma, element);
        var stiffnessMatrix = await _stiffnessMatrix.GetStiffnessMatrixAsync(testSession.Mu, element);

        for (var i = 0; i < element.Edges.Count; i++)
        {
            for (var j = 0; j < element.Edges.Count; j++)
            {
                await matrixProfile.AddElementToGlobalMatrixAsync(
                    element.Edges[i].EdgeIndex,
                    element.Edges[j].EdgeIndex,
                    stiffnessMatrix.Data[i][j]
                );
                await matrixProfile.AddElementToGlobalMatrixAsync(
                    element.Edges[i].EdgeIndex,
                    element.Edges[j].EdgeIndex,
                    massMatrix.Data[i][j]
                );
            }
        }
    }
}
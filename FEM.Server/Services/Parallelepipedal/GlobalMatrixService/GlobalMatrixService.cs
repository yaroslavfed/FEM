using FEM.Common.Data.MathModels;
using FEM.Common.Data.MeshModels;
using FEM.NonStationary.DTO.TestingContext;
using FEM.Server.Models.Parallelepipedal.MassMatrix;
using FEM.Server.Models.Parallelepipedal.StiffnessMatrix;

namespace FEM.Server.Services.Parallelepipedal.GlobalMatrixService;

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
    public async Task GetGlobalMatrixAsync(IMatrixFormat matrixProfile, NonStationaryTestSession<Mesh> nonStationaryTestSession)
    {
        foreach (var element in nonStationaryTestSession.Mesh.Elements)
        {
            await ResolveGlobalMatrixAsync(matrixProfile, element, nonStationaryTestSession);
        }
    }

    /// <summary>
    /// Собираем глобальную матрицу в заданном формате хранения
    /// </summary>
    /// <param name="matrixProfile">Выбранный формат хранения</param>
    /// <param name="element">Выбранный КЭ</param>
    /// <param name="nonStationaryTestSession"><see cref="NonStationaryNonStationaryTestSession{TMesh}"/></param>
    private async Task ResolveGlobalMatrixAsync(
        IMatrixFormat matrixProfile,
        FiniteElement element,
        NonStationaryTestSession<Mesh> nonStationaryTestSession
    )
    {
        var massMatrix = await _massMatrix.GetMassMatrixAsync(nonStationaryTestSession.Gamma, element);
        var stiffnessMatrix = await _stiffnessMatrix.GetStiffnessMatrixAsync(nonStationaryTestSession.Mu, element);

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
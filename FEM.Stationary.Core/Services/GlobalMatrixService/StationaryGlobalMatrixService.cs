using FEM.Common.Core.Services.BaseMatrixServices.MassMatrix;
using FEM.Common.Core.Services.BaseMatrixServices.StiffnessMatrix;
using FEM.Common.Core.Services.GlobalMatrixService;
using FEM.Common.DTO.Abstractions;
using FEM.Common.DTO.Models.MathModels;
using FEM.Common.DTO.Models.MatrixFormats;
using FEM.Common.DTO.Models.MeshModels;
using FEM.Stationary.DTO.TestingContext;

namespace FEM.Stationary.Core.Services.GlobalMatrixService;

/// <summary>
/// Сервис построения глобальной матрицы для стационарной системы
/// </summary>
public class StationaryGlobalMatrixService : IGlobalMatrixServices
{
    private readonly IStiffnessMatrix<Matrix> _stiffnessMatrix;
    private readonly IMassMatrix<Matrix>      _massMatrix;

    /// <summary>
    /// <see cref="StationaryGlobalMatrixService"/>
    /// </summary>
    /// <param name="stiffnessMatrix">Матрица жесткости</param>
    /// <param name="massMatrix">Матрица масс</param>
    public StationaryGlobalMatrixService(IStiffnessMatrix<Matrix> stiffnessMatrix, IMassMatrix<Matrix> massMatrix)
    {
        _stiffnessMatrix = stiffnessMatrix;
        _massMatrix = massMatrix;
    }

    /// <inheritdoc cref="IGlobalMatrixServices.GetGlobalMatrixAsync"/>
    public async Task GetGlobalMatrixAsync(MatrixProfileFormat matrixProfile, TestSessionBase testSession)
    {
        foreach (var element in testSession.Mesh.Elements)
        {
            await ResolveGlobalMatrixAsync(matrixProfile, element, testSession);
        }
    }

    /// <summary>
    /// Собираем глобальную матрицу в заданном формате хранения
    /// </summary>
    /// <param name="matrixProfile"><see cref="MatrixProfileFormat">Формат хранения</see></param>
    /// <param name="element">Выбранный КЭ</param>
    /// <param name="testSession"><see cref="TestSessionBase"/></param>
    private async Task ResolveGlobalMatrixAsync(
        MatrixProfileFormat matrixProfile,
        FiniteElement element,
        TestSessionBase testSession
    )
    {
        var stationaryTestSession = (StationaryTestSession)testSession;
        
        var massMatrix = await _massMatrix.GetMassMatrixAsync(stationaryTestSession.Gamma, element);
        var stiffnessMatrix = await _stiffnessMatrix.GetStiffnessMatrixAsync(stationaryTestSession.Mu, element);

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
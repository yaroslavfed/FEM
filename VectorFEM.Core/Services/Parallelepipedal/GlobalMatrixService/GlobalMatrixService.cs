using FEM.Common.Data.MathModels;
using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Common.Data.TestSession;
using FEM.Common.Enums;
using VectorFEM.Core.Data.Parallelepipedal;
using VectorFEM.Core.Models.Parallelepipedal.MassMatrix;
using VectorFEM.Core.Models.Parallelepipedal.StiffnessMatrix;
using VectorFEM.Core.Services.Parallelepipedal.MatrixPortraitService;
using VectorFEM.Core.Services.Parallelepipedal.RightPartVectorService;
using VectorFEM.Core.Services.TestSessionService;

namespace VectorFEM.Core.Services.Parallelepipedal.GlobalMatrixService;

public class GlobalMatrixService : IGlobalMatrixServices
{
    private readonly IMatrixPortraitService   _portraitService;
    private readonly IRightPartVectorService  _rightPartVectorService;
    private readonly ITestSessionService      _testSessionService;
    private readonly IStiffnessMatrix<Matrix> _stiffnessMatrix;
    private readonly IMassMatrix<Matrix>      _massMatrix;

    public GlobalMatrixService(
        IRightPartVectorService rightPartVectorService,
        IMatrixPortraitService portraitService,
        ITestSessionService testSessionService,
        IStiffnessMatrix<Matrix> stiffnessMatrix,
        IMassMatrix<Matrix> massMatrix
    )
    {
        _testSessionService = testSessionService;
        _rightPartVectorService = rightPartVectorService;
        _stiffnessMatrix = stiffnessMatrix;
        _massMatrix = massMatrix;
        _portraitService = portraitService;
    }

    public async Task<IMatrixFormat> GetGlobalMatrixProfileAsync(EMatrixFormats matrixFormat)
    {
        var testSession = await _testSessionService.CreateTestSessionAsync();

        var matrixProfile = await _portraitService.ResolveMatrixPortraitAsync(testSession.Mesh, matrixFormat);
        await matrixProfile.InitializeVectorsAsync(
            testSession.Mesh.Elements.SelectMany(element => element.Edges).DistinctBy(edge => edge.EdgeIndex).Count()
        );

        foreach (var element in testSession.Mesh.Elements)
        {
            await ResolveGlobalMatrixAsync(matrixProfile, element, testSession);
            await ResolveRightPartVectorAsync(matrixProfile, element, testSession);
        }

        return matrixProfile;
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

    /// <summary>
    /// Собираем вектор правой части в заданном формате хранения
    /// </summary>
    /// <param name="matrixProfile">Выбранный формат хранения</param>
    /// <param name="element">Выбранный КЭ</param>
    /// <param name="testSession"><see cref="TestSession{TMesh}"/></param>
    private async Task ResolveRightPartVectorAsync(
        IMatrixFormat matrixProfile,
        FiniteElement element,
        TestSession<Mesh> testSession
    )
    {
        var nodesList = element.Edges.SelectMany(edge => edge.Nodes).Distinct().ToArray();
        var firstNode = nodesList[0];
        var lastNode = nodesList[7];

        var hx = lastNode.Coordinate.X - firstNode.Coordinate.X;
        var hy = lastNode.Coordinate.Y - firstNode.Coordinate.Y;
        var hz = lastNode.Coordinate.Z - firstNode.Coordinate.Z;

        var localRightPartAsync = await ResolveLocalRightPartAsync(hx, hy, hz, element, testSession);

        for (var i = 0; i < element.Edges.Count; i++)
        {
            await matrixProfile.AddElementToRightPartAsync(element.Edges[i].EdgeIndex, localRightPartAsync[i]);
        }
    }

    /// <summary>
    /// Заполняем вектор правой части
    /// </summary>
    /// <param name="hx">Шаг по OX</param>
    /// <param name="hy">Шаг по OY</param>
    /// <param name="hz">Шаг по OZ</param>
    /// <param name="element">Конечный элемент расчётной области</param>
    /// <param name="testSession">Сессия расчёта области</param>
    /// <returns>Вектор правой части</returns>
    private async Task<IList<double>> ResolveLocalRightPartAsync(
        double hx,
        double hy,
        double hz,
        FiniteElement element,
        TestSession<Mesh> testSession
    )
    {
        List<double> localRightPart = [..Enumerable.Range(0, 12).Select(_ => 0)];
        var tempLocalRightPart = new List<double>();

        for (var i = 0; i < localRightPart.Count; i++)
            tempLocalRightPart.Add(
                await _rightPartVectorService.ResolveRightPartValueAsync(element.Edges[i], testSession)
            );

        var coefficient = hx * hy * hz / 36.0;

        for (var i = 0; i < _massMatrix.MassMatrixBase.Count; i++)
            for (var j = 0; j < _massMatrix.MassMatrixBase.Count; j++)
                localRightPart[i] += coefficient * _massMatrix.MassMatrixBase[i][j] * tempLocalRightPart[j];

        return localRightPart;
    }
}
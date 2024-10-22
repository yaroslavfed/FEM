using FEM.Common.Core.Services.BaseMatrixServices.MassMatrix;
using FEM.Common.Core.Services.ProblemService;
using FEM.Common.Core.Services.RightPartVectorService;
using FEM.Common.DTO.Abstractions;
using FEM.Common.DTO.Domain;
using FEM.Common.DTO.Models.MathModels;
using FEM.Common.DTO.Models.MatrixFormats;
using FEM.Common.DTO.Models.MeshModels;
using FEM.Stationary.DTO.TestingContext;

namespace FEM.Stationary.Core.Services.RightPartVectorService;

// TODO: надо бы переделать класс, не нравится мне эта перегонка в локальные из глобальных, выглядит бесполезной
/// <inheritdoc cref="IRightPartVectorService"/>
public class RightPartVectorService : IRightPartVectorService
{
    private readonly IProblemService     _problemService;
    private readonly IMassMatrix<Matrix> _massMatrix;

    public RightPartVectorService(IProblemService problemService, IMassMatrix<Matrix> massMatrix)
    {
        _problemService = problemService;
        _massMatrix = massMatrix;
    }

    /// <inheritdoc cref="IRightPartVectorService.GetRightPartVectorAsync"/>
    public async Task GetRightPartVectorAsync(MatrixProfileFormat matrixProfile, TestSessionBase testSession)
    {
        foreach (var element in testSession.Mesh.Elements)
        {
            await ResolveRightPartVectorAsync(matrixProfile, element, testSession);
        }
    }

    /// <summary>
    /// Расчет значений элементов вектора правой части
    /// </summary>
    private async Task<double> ResolveRightPartValueAsync(Edge edge, StationaryTestSession testSession)
    {
        var localNodes = await _problemService.ResolveLocalNodes(edge, testSession);

        var vectorContributions = await _problemService.ResolveVectorContributionsAsync(
            (localNodes.firstNode, localNodes.secondNode),
            localNodes.direction
        );
        var matrixContributions = await _problemService.ResolveMatrixContributionsAsync(
            (localNodes.firstNode, localNodes.secondNode),
            localNodes.direction
        );

        return 1.0 / testSession.Mu * vectorContributions + testSession.Gamma * matrixContributions;
    }

    /// <summary>
    /// Собираем вектор правой части в заданном формате хранения
    /// </summary>
    /// <param name="matrixProfile">Выбранный формат хранения</param>
    /// <param name="element">Выбранный КЭ</param>
    /// <param name="testSession"><see cref="TestSessionBase"/></param>
    private async Task ResolveRightPartVectorAsync(
        MatrixProfileFormat matrixProfile,
        FiniteElement element,
        TestSessionBase testSession
    )
    {
        var stationaryTestSession = (StationaryTestSession)testSession;

        var nodesList = element.Edges.SelectMany(edge => edge.Nodes).Distinct().ToArray();
        var firstNode = nodesList[0];
        var lastNode = nodesList[7];

        var hx = lastNode.Coordinate.X - firstNode.Coordinate.X;
        var hy = lastNode.Coordinate.Y - firstNode.Coordinate.Y;
        var hz = lastNode.Coordinate.Z - firstNode.Coordinate.Z;

        var localRightPartAsync = await ResolveLocalRightPartAsync(hx, hy, hz, element, stationaryTestSession);

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
        StationaryTestSession testSession
    )
    {
        List<double> localRightPart = [..Enumerable.Range(0, 12).Select(_ => 0)];
        var tempLocalRightPart = new List<double>();

        for (var i = 0; i < localRightPart.Count; i++)
            tempLocalRightPart.Add(await ResolveRightPartValueAsync(element.Edges[i], testSession));

        var coefficient = hx * hy * hz / 36.0;

        for (var i = 0; i < _massMatrix.MassMatrixBase.Count; i++)
        for (var j = 0; j < _massMatrix.MassMatrixBase.Count; j++)
            localRightPart[i] += coefficient * _massMatrix.MassMatrixBase[i][j] * tempLocalRightPart[j];

        return localRightPart;
    }
}
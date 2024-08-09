using FEM.Common.Data.MathModels;
using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Common.Data.TestSession;
using FEM.Common.Enums;
using VectorFEM.Core.Data.Parallelepipedal;
using VectorFEM.Core.Models.Parallelepipedal.MassMatrix;
using VectorFEM.Core.Models.Parallelepipedal.StiffnessMatrix;
using VectorFEM.Core.Services.Parallelepipedal.MatrixPortraitService;
using VectorFEM.Core.Services.Parallelepipedal.MeshService;
using VectorFEM.Core.Services.Parallelepipedal.RightPartVectorService;
using VectorFEM.Core.Services.TestSessionService;

namespace VectorFEM.Core.Services.Parallelepipedal.GlobalMatrixService;

public class GlobalMatrixService : IGlobalMatrixServices
{
    private readonly IMatrixPortraitService   _portraitService;
    private readonly IRightPartVectorService  _rightPartVectorService;
    private readonly IMeshService             _meshService;
    private readonly ITestSessionService      _testSessionService;
    private readonly IStiffnessMatrix<Matrix> _stiffnessMatrix;
    private readonly IMassMatrix<Matrix>      _massMatrix;

    public GlobalMatrixService(
        IMeshService meshService,
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
        _meshService = meshService;
    }

    public async Task<IMatrixFormat> GetGlobalMatrixAsync(EMatrixFormats matrixFormat)
    {
        var testSession = await _testSessionService.CreateTestSessionAsync();

        var matrixProfile = await _portraitService.ResolveMatrixPortraitAsync(testSession.Mesh, matrixFormat);

        foreach (var element in testSession.Mesh.Elements)
        {
            var firstNode = (from edge in element.Edges
                             from node in edge.Nodes
                             where node.NodeIndex == 0
                             select node.Coordinate)
                            .ToArray()
                            .Single();

            var lastNode = (from edge in element.Edges
                            from node in edge.Nodes
                            where node.NodeIndex == 7
                            select node.Coordinate)
                           .ToArray()
                           .Single();

            var hx = lastNode.X - firstNode.X;
            var hy = lastNode.Y - firstNode.Y;
            var hz = lastNode.Z - firstNode.Z;

            var massMatrix = await _massMatrix.GetMassMatrixAsync(testSession.Gamma);
            var stiffnessMatrix = await _stiffnessMatrix.GetStiffnessMatrixAsync(testSession.Mu);
            var rightPartVector = await ResolveLocalRightPartAsync(hx, hy, hz, element, testSession);

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

                await matrixProfile.AddElementToRightPartAsync(element.Edges[i].EdgeIndex, rightPartVector[i]);
            }
        }

        return matrixProfile;
    }

    /// <summary>
    /// Заполняем вектор правой части
    /// </summary>
    /// <param name="hx">Шаг по OX</param>
    /// <param name="hy">Шаг по OY</param>
    /// <param name="hz">Шаг по OZ</param>
    /// <param name="element">Конечный элемент расчётной области</param>
    /// <returns>Вектор правой части</returns>
    private async Task<IList<double>> ResolveLocalRightPartAsync(double hx, double hy, double hz, FiniteElement element, TestSession<Mesh> testSession)
    {
        List<double> localRightPart = [..Enumerable.Range(0, 12).Select(item => 0)];
        var tempLocalRightPart = new List<double>();

        for (int i = 0; i < localRightPart.Count; i++)
            tempLocalRightPart.Add(await _rightPartVectorService.ResolveRightPartValueAsync(element.Edges[i], testSession));

        double coefficient = hx * hy * hz / 36.0;

        for (int i = 0; i < _massMatrix.MassMatrixBase.Count; i++)
            for (int j = 0; j < _massMatrix.MassMatrixBase.Count; j++)
                localRightPart[i] += coefficient * _massMatrix.MassMatrixBase[i][j] * tempLocalRightPart[j];

        return localRightPart;
    }
}
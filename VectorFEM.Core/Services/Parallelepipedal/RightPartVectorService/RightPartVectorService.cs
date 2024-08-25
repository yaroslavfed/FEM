using FEM.Common.Data.Domain;
using VectorFEM.Core.Data.Parallelepipedal;
using FEM.Common.Data.TestSession;
using VectorFEM.Core.Services.TestingService;

namespace VectorFEM.Core.Services.Parallelepipedal.RightPartVectorService;

// TODO: надо бы переделать класс, не нравится мне эта перегонка в локальные из глобальных, выглядит бесполезной
/// <inheritdoc cref="IRightPartVectorService"/>
public class RightPartVectorService : IRightPartVectorService
{
    private readonly ITestingService _testingService;

    public RightPartVectorService(ITestingService testingService)
    {
        _testingService = testingService;
    }

    public async Task<double> ResolveRightPartValueAsync(Edge edge, TestSession<Mesh> testSession)
    {
        var test = await _testingService.ResolveLocalNodes(edge, testSession);

        var vectorContributions = await _testingService.ResolveVectorContributionsAsync(
            (test.firstNode, test.secondNode),
            test.direction
        );
        var matrixContributions = await _testingService.ResolveMatrixContributions(
            (test.firstNode, test.secondNode),
            test.direction
        );

        return 1.0 / testSession.Mu * vectorContributions + testSession.Gamma * matrixContributions;
    }
}
using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Common.Data.TestSession;
using VectorFEM.Core.Data.Parallelepipedal;
using VectorFEM.Core.Services.TestingService;

namespace VectorFEM.Core.Services.Parallelepipedal.BoundaryConditionService.BoundaryConditions;

public class FirstBoundaryConditionService : IBoundaryConditionService
{
    private ITestingService _testingService;

    public FirstBoundaryConditionService(ITestingService testingService)
    {
        _testingService = testingService;
    }

    public async Task<IMatrixFormat> AddBoundaryCondition(TestSession<Mesh> testSession)
    {
        var nodesList = await GetNodesListAsync(testSession);

        var nx = nodesList.Select(node => node.Coordinate.X).Distinct().Count();
        var ny = nodesList.Select(node => node.Coordinate.Y).Distinct().Count();
        var nz = nodesList.Select(node => node.Coordinate.Z).Distinct().Count();

        var gr = nx * (ny - 1) + ny * (nx - 1);
        var pop = nx * ny;

        var n = Enumerable.Range(0, 4).ToArray();
        var boundaryConditionsList = new List<(int nodeIndex, double value)>();

        for (var i = 0; i < nz - 1; i++)
            for (var j = 0; j < ny - 1; j++)
            {
                n[0] = i * (gr + pop) + j * (2 * nx - 1) + (nx - 1);
                n[1] = i * (gr + pop) + gr + j * nx;
                n[2] = i * (gr + pop) + gr + j * nx + nx;
                n[3] = (i + 1) * (gr + pop) + j * (2 * nx - 1) + (nx - 1);

                boundaryConditionsList.AddRange(await FillBoundaryConditionsList(n, boundaryConditionsList));
            }

        return new MatrixProfileFormat();
    }

    private async Task<IList<(int nodeIndex, double nodeValue)>> FillBoundaryConditionsList(
        IList<int> list,
        IList<(int nodeIndex, double nodeValue)> boundaryConditionsList
    )
    {
        for (var index = 0; index < 4; index += 3)
        {
            if (!IsInBoundary(list[index], boundaryConditionsList))
            {
                var localNodes = await _testingService.ResolveLocalNodes(edge, testSession);
                var contributionValue = await _testingService.ResolveMatrixContributions()
                boundaryConditionsList.Add((list[index], contributionValue));
            }
        }

        for (var index = 1; index < 3; index++)
        {
            if (!IsInBoundary(list[index], boundaryConditionsList))
            {
                boundaryConditionsList.Add((list[index], (list[index])));
            }
        }
    }

    private static bool IsInBoundary(int num, IList<(int nodeIndex, double value)> boundaryConditionsList)
    {
        for (var i = 0; i < boundaryConditionsList.Count; i++)
            if (boundaryConditionsList[i].nodeIndex == num)
                return true;

        return false;
    }

    private Task<Node[]> GetNodesListAsync(TestSession<Mesh> testSession)
    {
        var localNode = testSession
                        .Mesh
                        .Elements
                        .SelectMany(element => element.Edges.SelectMany(edge => edge.Nodes))
                        .ToArray();

        return Task.FromResult(localNode);
    }
}
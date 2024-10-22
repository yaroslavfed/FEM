using FEM.Common.Core.Services.ProblemService;
using FEM.Common.DTO.Abstractions;
using FEM.Common.DTO.Domain;
using FEM.Common.DTO.Models.MatrixFormats;

namespace FEM.Common.Core.Services.BoundaryConditionService.BoundaryConditions;

/// <summary>
/// Сервис первых краевых условий
/// </summary>
public class FirstBoundaryConditionService : IBoundaryConditionService
{
    private readonly IProblemService _problemService;

    /// <inheritdoc cref="FirstBoundaryConditionService"/>
    /// <param name="problemService">Сервис решаемого уравнения</param>
    public FirstBoundaryConditionService(IProblemService problemService)
    {
        _problemService = problemService;
    }

    /// <inheritdoc />
    public async Task SetBoundaryConditionsAsync(TestSessionBase testSession, MatrixProfileFormat matrixProfile)
    {
        var boundaryConditionsList = await AddBoundaryCondition(testSession);
        var boundaryNodesList = Enumerable
                                .Range(
                                    0,
                                    testSession
                                        .Mesh
                                        .Elements
                                        .SelectMany(element => element.Edges)
                                        .DistinctBy(node => node.EdgeIndex)
                                        .Count()
                                )
                                .Select(_ => -1)
                                .ToArray();

        for (var i = 0; i < boundaryConditionsList.Count; i++)
            boundaryNodesList[boundaryConditionsList[i].index] = i;

        for (var i = 0; i < boundaryNodesList.Length; i++)
            if (boundaryNodesList[i] != -1)
            {
                matrixProfile.Di[i] = 1.0;
                matrixProfile.F[i] = boundaryConditionsList[boundaryNodesList[i]].value;
                for (var j = matrixProfile.Ig[i]; j < matrixProfile.Ig[i + 1]; j++)
                {
                    var k = matrixProfile.Jg[j];
                    if (boundaryNodesList[k] == -1)
                        matrixProfile.F[k] -= matrixProfile.Gg[j] * matrixProfile.F[i];

                    matrixProfile.Gg[j] = 0.0;
                }
            }
            else
                for (var j = matrixProfile.Ig[i]; j < matrixProfile.Ig[i + 1]; j++)
                {
                    var k = matrixProfile.Jg[j];
                    if (boundaryNodesList[k] == -1)
                        continue;

                    matrixProfile.F[i] -= matrixProfile.Gg[j] * matrixProfile.F[k];
                    matrixProfile.Gg[j] = 0.0;
                }
    }

    private async Task<List<(int index, double value)>> AddBoundaryCondition(TestSessionBase testSession)
    {
        var nodesList = await GetNodesListAsync(testSession);

        var nx = nodesList.Select(node => node.Coordinate.X).Distinct().Count();
        var ny = nodesList.Select(node => node.Coordinate.Y).Distinct().Count();
        var nz = nodesList.Select(node => node.Coordinate.Z).Distinct().Count();

        var gr = nx * (ny - 1) + ny * (nx - 1);
        var pop = nx * ny;

        var n = Enumerable.Range(0, 4).ToArray();
        var boundaryConditionsList = new List<(int index, double value)>();

        // Заполняем краевые условия для левой грани КЭ
        for (var i = 0; i < nz - 1; i++)
        for (var j = 0; j < ny - 1; j++)
        {
            n[0] = i * (gr + pop) + j * (2 * nx - 1) + (nx - 1);
            n[1] = i * (gr + pop) + gr + j * nx;
            n[2] = i * (gr + pop) + gr + j * nx + nx;
            n[3] = (i + 1) * (gr + pop) + j * (2 * nx - 1) + (nx - 1);

            await FillBoundaryConditionsList(n, boundaryConditionsList, testSession);
        }

        // Заполняем краевые условия для правой грани КЭ
        for (var i = 0; i < nz - 1; i++)
        for (var j = 0; j < ny - 1; j++)
        {
            n[0] = i * (gr + pop) + j * (2 * nx - 1) + (nx - 2) + 1 + (nx - 1);
            n[1] = i * (gr + pop) + gr + (nx - 2) + j * nx + 1;
            n[2] = i * (gr + pop) + gr + (nx - 2) + j * nx + nx + 1;
            n[3] = (i + 1) * (gr + pop) + j * (2 * nx - 1) + (nx - 1) + (nx - 2) + 1;

            await FillBoundaryConditionsList(n, boundaryConditionsList, testSession);
        }

        // Заполняем краевые условия для нижней грани КЭ
        for (var i = 0; i < ny - 1; i++)
        for (var j = 0; j < nx - 1; j++)
        {
            n[0] = i * (2 * nx - 1) + j;
            n[1] = i * (2 * nx - 1) + j + (nx - 1);
            n[2] = i * (2 * nx - 1) + j + (nx - 1) + 1;
            n[3] = (i + 1) * (2 * nx - 1) + j;

            await FillBoundaryConditionsList(n, boundaryConditionsList, testSession);
        }

        // Заполняем краевые условия для верхней грани КЭ
        for (var i = 0; i < ny - 1; i++)
        for (var j = 0; j < nx - 1; j++)
        {
            n[0] = (nz - 1) * (gr + pop) + i * (2 * nx - 1) + j;
            n[1] = (nz - 1) * (gr + pop) + i * (2 * nx - 1) + (nx - 1) + j;
            n[2] = (nz - 1) * (gr + pop) + i * (2 * nx - 1) + (nx - 1) + j + 1;
            n[3] = (nz - 1) * (gr + pop) + (i + 1) * (2 * nx - 1) + j;

            await FillBoundaryConditionsList(n, boundaryConditionsList, testSession);
        }

        // Заполняем краевые условия для передней грани КЭ
        for (var i = 0; i < nz - 1; i++)
        for (var j = 0; j < nx - 1; j++)
        {
            n[0] = i * (gr + pop) + j;
            n[1] = i * (gr + pop) + gr + j;
            n[2] = i * (gr + pop) + gr + j + 1;
            n[3] = (i + 1) * (gr + pop) + j;

            await FillBoundaryConditionsList(n, boundaryConditionsList, testSession);
        }

        // Заполняем краевые условия для задней грани КЭ
        for (var i = 0; i < nz - 1; i++)
        for (var j = 0; j < nx - 1; j++)
        {
            n[0] = i * (gr + pop) + (ny - 2 + 1) * (2 * nx - 1) + j;
            n[1] = i * (gr + pop) + gr + j + (ny - 2) * nx + nx;
            n[2] = i * (gr + pop) + gr + j + (ny - 2) * nx + nx + 1;
            n[3] = (i + 1) * (gr + pop) + (ny - 2 + 1) * (2 * nx - 1) + j;

            await FillBoundaryConditionsList(n, boundaryConditionsList, testSession);
        }

        boundaryConditionsList.Sort((first, second) => first.index.CompareTo(second.index));

        return boundaryConditionsList;
    }

    private async Task FillBoundaryConditionsList(
        IList<int> list,
        List<(int nodeIndex, double nodeValue)> boundaryConditionsList,
        TestSessionBase testSession
    )
    {
        for (var index = 0; index < 4; index += 3)
        {
            if (IsInBoundary(list[index], boundaryConditionsList))
                continue;

            var contributionValue = await CalculateContributionValueAsync(testSession, list[index]);
            boundaryConditionsList.Add((list[index], contributionValue));
        }

        for (var index = 1; index < 3; index++)
        {
            if (IsInBoundary(list[index], boundaryConditionsList))
                continue;

            var contributionValue = await CalculateContributionValueAsync(testSession, list[index]);
            boundaryConditionsList.Add((list[index], contributionValue));
        }
    }

    private async Task<double> CalculateContributionValueAsync(TestSessionBase testSession, int edgeIndex)
    {
        var edge = testSession
                   .Mesh
                   .Elements
                   .SelectMany(element => element.Edges)
                   .FirstOrDefault(edge => edge.EdgeIndex == edgeIndex)
                   ?? throw new("Edge with this index did not find");

        var localNodes = await _problemService.ResolveLocalNodes(edge, testSession);
        var contributionValue = await _problemService.ResolveMatrixContributionsAsync(
            (localNodes.firstNode, localNodes.secondNode),
            localNodes.direction
        );

        return contributionValue;
    }

    private static bool IsInBoundary(int num, IList<(int nodeIndex, double value)> boundaryConditionsList)
    {
        for (var i = 0; i < boundaryConditionsList.Count; i++)
            if (boundaryConditionsList[i].nodeIndex == num)
                return true;

        return false;
    }

    private static Task<Node[]> GetNodesListAsync(TestSessionBase testSession)
    {
        var localNode = testSession
                        .Mesh
                        .Elements
                        .SelectMany(element => element.Edges.SelectMany(edge => edge.Nodes))
                        .ToArray();

        return Task.FromResult(localNode);
    }
}
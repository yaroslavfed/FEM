using FEM.Common.Data.MathModels.MatrixFormats;
using VectorFEM.Core.Services.TestingService;

namespace VectorFEM.Core.Services.Parallelepipedal.BoundaryConditionService.BoundaryConditions;

public class FirstBoundaryConditionService : IBoundaryConditionService
{
    private ITestingService _testingService;

    public FirstBoundaryConditionService(ITestingService testingService)
    {
        _testingService = testingService;
    }

    // public Task<IMatrixFormat> AddBoundaryCondition(int nx, int ny, int nz)
    // {
    //     int gr = nx * (ny - 1) + ny * (nx - 1);
    //     int pop = nx * ny;
    // }

    // private async Task<IReadOnlyList<(int nodeIndex, double nodeValue)>> FillBoundaryConditionsList(
    //     IList<(int nodeIndex, double value)> boundaryConditionsList,
    //     IReadOnlyList<int> list
    // )
    // {
    //     for (int i = 0; i < 4; i += 3)
    //     {
    //         if (!IsInBoundary(list[i], boundaryConditionsList))
    //         {
    //             var contributionValue = await _testingService.ResolveMatrixContributions()
    //             boundaryConditionsList.Add((list[i], contributionValue));
    //         }
    //     }
    //
    //     for (int i = 1; i < 3; i++)
    //     {
    //         if (!IsInBoundary(list[i], boundaryConditionsList))
    //         {
    //             boundaryConditionsList.Add((list[i], bc1_value(list[i])));
    //         }
    //     }
    // }

    private static bool IsInBoundary(int num, IList<(int nodeIndex, double value)> boundaryConditionsList)
    {
        for (var i = 0; i < boundaryConditionsList.Count; i++)
            if (boundaryConditionsList[i].nodeIndex == num)
                return true;

        return false;
    }
}
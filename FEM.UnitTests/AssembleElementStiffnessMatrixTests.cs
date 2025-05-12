using FEM.Server.Services.BasisFunctionProvider;
using FEM.Server.Services.ProblemService;
using Xunit;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

namespace FEM.UnitTests;

public class AssembleElementStiffnessMatrixTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public AssembleElementStiffnessMatrixTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task AssembleElementStiffnessMatrix_CubeMu0_ValuesAreInExpectedRange()
    {
        // Arrange
        const double mu0 = 4 * Math.PI * 1e-7;

        var cube = FiniteElementFactory.CreateUnitCube(mu0);
        var basis = new BasicFunctionProvider();

        var service = new ProblemService(basis);

        // Act
        var matrix = await service.AssembleElementStiffnessMatrixAsync(cube);

        // Assert
        Assert.Equal(12, matrix.Rows);
        Assert.Equal(12, matrix.Columns);

        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                var v = matrix[i, j];
                Assert.False(double.IsNaN(v), $"NaN at ({i},{j})");
                Assert.False(double.IsInfinity(v), $"Infinity at ({i},{j})");
                Assert.InRange(v, -1e-4, 1e-4);
            }
        }
        
        for (int i = 0; i < 12; i++)
            _testOutputHelper.WriteLine(
                string.Join("\t", Enumerable.Range(0, 12).Select(j => matrix[i, j].ToString("E3")))
            );
    }
}
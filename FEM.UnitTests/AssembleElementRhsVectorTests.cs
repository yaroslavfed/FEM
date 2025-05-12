using FEM.Server.Data.Domain;
using FEM.Server.Models.BasicFunction;
using FEM.Server.Services.ProblemService;
using FEM.Server.Services.SourceProvider;
using Xunit;
using Xunit.Abstractions;
using Assert = Xunit.Assert;

namespace FEM.UnitTests;

public class AssembleElementRhsVectorTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public AssembleElementRhsVectorTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public async Task AssembleElementRightHandVector_ComputesExpectedContributions()
    {
        // Arrange
        const double mu0 = 4 * Math.PI * 1e-7;

        var cube = FiniteElementFactory.CreateUnitCube(mu0);
        var basis = new BasicFunctionProviderStub();
        var rhsService = new ProblemService(basis);
        var sourceServer = new CurrentSourceProvider();

        var source = new CurrentSource()
        {
            Start = new(0.5, 0.5, 0.0), End = new(0.5, 0.5, 1.0), Amperage = 1.0, Segments = 1
        };

        // Act
        var sources = await sourceServer.GetSourcesAsync(source);
        var vector = await rhsService.AssembleElementRightHandVectorAsync(cube, sources);

        // Assert
        for (int i = 0; i < vector.Size; i++)
        {
            Assert.False(double.IsNaN(vector[i]), $"NaN at ({i})");
            Assert.False(double.IsInfinity(vector[i]), $"Infinity at ({i})");
            double maxExpected = 1.0 / (4 * Math.PI * 1e-7);
            Assert.InRange(vector[i], -2 * maxExpected, 2 * maxExpected);
        }

        _testOutputHelper.WriteLine(string.Join("\t", Enumerable.Range(0, 12).Select(i => vector[i].ToString("E3"))));
    }
}
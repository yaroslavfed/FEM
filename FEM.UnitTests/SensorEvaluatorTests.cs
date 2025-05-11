using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Services.SensorEvaluator;
using Xunit;
using Assert = Xunit.Assert;

namespace FEM.UnitTests;

public class SensorEvaluatorTests
{
    [Fact]
    public void EvaluateBVectorAt_CenterOfUniformElement_ReturnsExpectedB()
    {
        // Arrange
        var element = TestElementFactory.CreateUnitCube(mu: 1.0); // куб с μ = 1
        var mesh = new Mesh { Elements = [element] };

        // Вектор A: просто константы на каждом ребре, например, A_i = 1
        var solution = new Vector(12);
        for (int i = 0; i < 12; i++) solution[i] = 1.0;

        var center = element.GetCenter();
        var basis = new BasicFunctionProviderStub(); // Подставной, возвращающий аналитические curl
        var evaluator = new SensorEvaluator(basis);

        // Act
        var b = evaluator.EvaluateFullBAtPoint(center, mesh, solution);

        // Assert
        Assert.False(double.IsNaN(b.X) || double.IsInfinity(b.X));
        Assert.False(double.IsNaN(b.Y) || double.IsInfinity(b.Y));
        Assert.False(double.IsNaN(b.Z) || double.IsInfinity(b.Z));

        // Пример грубой проверки ожидаемого диапазона
        Assert.InRange(b.X, -10, 10);
        Assert.InRange(b.Y, -10, 10);
        Assert.InRange(b.Z, -10, 10);
    }
}
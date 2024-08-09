using FEM.Common.Data.MathModels;
using FEM.Common.Enums;

namespace VectorFEM.Core.Services.TestingService;

/// <inheritdoc cref="ITestingService"/>
public class TestingService : ITestingService
{
    public Task<double> ResolveRightPartVector(Point3D coordinate, double mu, double gamma, EDirections direction)
    {
        var vectorF = new Vector
        {
            Data = [
                8 * (coordinate.Y + coordinate.Z),
                8 * (coordinate.X + coordinate.Z),
                8 * (coordinate.X + coordinate.Y)
            ]
        };

        var result = 1 / mu * direction switch
        {
            EDirections.OX => vectorF.Data[0],
            EDirections.OY => vectorF.Data[1],
            EDirections.OZ => vectorF.Data[2],
            _ => throw new NotImplementedException()
        };

        return Task.FromResult(result);
    }
}
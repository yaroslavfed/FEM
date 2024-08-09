using FEM.Common.Data.MathModels;
using FEM.Common.Enums;

namespace VectorFEM.Core.Services.TestingService;

public interface ITestingService
{
    Task<double> ResolveRightPartVector(Point3D coordinate, double mu, double gamma, EDirections direction);
}
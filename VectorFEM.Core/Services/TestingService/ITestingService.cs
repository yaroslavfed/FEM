using FEM.Common.Data.Domain;
using FEM.Common.Enums;

namespace VectorFEM.Core.Services.TestingService;

public interface ITestingService
{
    Task<double> ResolveMatrixContributions((Node firstNode, Node secondNode) nodesPair, EDirections direction);

    Task<double> ResolveVectorContributionsAsync(
        (Node firstNode, Node secondNode) nodesPair,
        double mu,
        double gamma,
        EDirections direction
    );
}
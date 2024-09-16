using FEM.Common.Data.Domain;
using FEM.Common.Data.TestSession;
using FEM.Common.Enums;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.TestingService;

public interface ITestingService
{
    Task<double> ResolveMatrixContributions((Node firstNode, Node secondNode) nodesPair, EDirections direction);

    Task<double> ResolveVectorContributionsAsync((Node firstNode, Node secondNode) nodesPair, EDirections direction);

    Task<(Node firstNode, Node secondNode, EDirections direction)> ResolveLocalNodes(
        Edge edge,
        TestSession<Mesh> testSession
    );
}
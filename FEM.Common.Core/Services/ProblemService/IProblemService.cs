using FEM.Common.DTO.Abstractions;
using FEM.Common.DTO.Domain;
using FEM.Common.DTO.Enums;

namespace FEM.Common.Core.Services.ProblemService;

public interface IProblemService
{
    Task<double> ResolveMatrixContributionsAsync((Node firstNode, Node secondNode) nodesPair, EDirections direction);

    Task<double> ResolveVectorContributionsAsync((Node firstNode, Node secondNode) nodesPair, EDirections direction);

    Task<(Node firstNode, Node secondNode, EDirections direction)> ResolveLocalNodes(
        Edge edge,
        TestSessionBase testSession
    );
}
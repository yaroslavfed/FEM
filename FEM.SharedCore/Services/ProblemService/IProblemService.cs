using FEM.SharedDTO.Abstractions;
using FEM.SharedDTO.Domain;
using FEM.SharedDTO.Enums;

namespace FEM.SharedCore.Services.ProblemService;

public interface IProblemService
{
    Task<double> ResolveMatrixContributionsAsync((Node firstNode, Node secondNode) nodesPair, EDirections direction);

    Task<double> ResolveVectorContributionsAsync((Node firstNode, Node secondNode) nodesPair, EDirections direction);

    Task<(Node firstNode, Node secondNode, EDirections direction)> ResolveLocalNodes(
        Edge edge,
        TestSessionBase testSession
    );
}
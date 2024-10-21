using FEM.Common.Data.Domain;
using FEM.Common.Data.MeshModels;
using FEM.Common.Enums;
using FEM.NonStationary.DTO.TestingContext;

namespace FEM.Server.Services.TestingService;

public interface IProblemService
{
    Task<double> ResolveMatrixContributionsAsync((Node firstNode, Node secondNode) nodesPair, EDirections direction);

    Task<double> ResolveVectorContributionsAsync((Node firstNode, Node secondNode) nodesPair, EDirections direction);

    Task<(Node firstNode, Node secondNode, EDirections direction)> ResolveLocalNodes(
        Edge edge,
        NonStationaryTestSession<Mesh> nonStationaryTestSession
    );
}
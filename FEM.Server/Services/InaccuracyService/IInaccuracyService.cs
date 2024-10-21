using FEM.Common.Data.MeshModels;
using FEM.NonStationary.DTO.TestingContext;
using FEM.Server.Data;
using FEM.Server.Data.OutputModels;

namespace FEM.Server.Services.InaccuracyService;

public interface IInaccuracyService
{
    Task GetSolutionVectorInaccuracy(NonStationaryTestSession<Mesh> nonStationaryTestSession, SolutionResult solutionResult);
}
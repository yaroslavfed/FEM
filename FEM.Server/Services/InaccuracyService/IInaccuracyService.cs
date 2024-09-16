using FEM.Common.Data.TestSession;
using FEM.Server.Data;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.InaccuracyService;

public interface IInaccuracyService
{
    Task GetSolutionVectorInaccuracy(TestSession<Mesh> testSession, SolutionResult solutionResult);
}
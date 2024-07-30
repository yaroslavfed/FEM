using FEM.Common.Data.TestSession;
using VectorFEM.Core.Data.Parallelepipedal;

namespace VectorFEM.Core.Services.TestSessionService;

public interface ITestSessionService
{
    Task<TestSession<Mesh>> CreateTestSessionAsync();
}
using FEM.Common.Data.TestSession;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.TestSessionService;

public interface ITestSessionService
{
    Task<TestSession<Mesh>> CreateTestSessionAsync();
}
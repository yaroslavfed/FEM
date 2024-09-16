using FEM.Common.Data.TestSession;
using FEM.Core.Data.Parallelepipedal;

namespace FEM.Core.Services.TestSessionService;

public interface ITestSessionService
{
    Task<TestSession<Mesh>> CreateTestSessionAsync();
}
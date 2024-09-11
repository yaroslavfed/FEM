using Client.Shared.Data;

namespace Client.Shared.Services.TestingService;

public interface ITestingService
{
    Task CreateSessionAsync(TestSession testSession);
}
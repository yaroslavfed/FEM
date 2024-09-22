using Client.Shared.HttpClientContext;

namespace Client.Shared.API.TestingServiceClient;

public interface ITestingServiceClient
{
    Task<FemResponse> CalculateSolutionVector(Data.TestSession testSession);

    Task<string> GetSessionResult(Guid id);
}
using AutoMapper;
using Client.Shared.HttpClientContext;

namespace Client.Shared.API;

public class TestingServiceClient : ITestingServiceClient
{
    private readonly IMapper   _mapper;
    private readonly FemClient _client;

    public TestingServiceClient(IMapper mapper)
    {
        _mapper = mapper;
        _client = new(new());
    }

    public async Task<FemResponse> CalculateSolutionVector(Data.TestSession testSession)
    {
        var testSessionContext = _mapper.Map<TestSession>(testSession);

        return await _client.CreateCalculationAsync(testSessionContext);
    }

    public async Task<string> GetSessionResult(Guid id)
    {
        return await _client.GetTestResultAsync(id);
    }
}
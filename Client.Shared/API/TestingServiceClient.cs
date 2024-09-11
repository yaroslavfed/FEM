using AutoMapper;
using Client.Shared.HttpClientContext;

namespace Client.Shared.API;

public class TestingServiceClient : ITestingServiceClient
{
    private readonly IMapper _mapper;

    public TestingServiceClient(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<FemResponse> CalculateSolutionVector(Data.TestSession testSession)
    {
        var client = new FemClient(new HttpClient());

        var testSessionContext = _mapper.Map<TestSession>(testSession);

        return await client.CreateCalculationAsync(testSessionContext);
    }
}
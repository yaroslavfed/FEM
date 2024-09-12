using System.Diagnostics;
using AutoMapper;
using Client.Shared.API;
using Client.Shared.HttpClientContext;

namespace Client.Shared.Services.TestingService;

public class TestingService : ITestingService
{
    private readonly IMapper _mapper;
    private readonly ITestingServiceClient _client;

    public TestingService(IMapper mapper, ITestingServiceClient client)
    {
        _mapper = mapper;
        _client = client;
    }

    public async Task<FemResponse> CreateSessionAsync(Data.TestSession testSession)
    {
        try
        {
            return await _client.CalculateSolutionVector(testSession);
        }
        catch (Exception e)
        {
            Debug.Fail(e.Message);
            throw;
        }
    }
}
using System.Diagnostics;
using AutoMapper;
using Client.Shared.API;

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

    public async Task CreateSessionAsync(Data.TestSession testSession)
    {
        try
        {
            var response = await _client.CalculateSolutionVector(testSession);
        }
        catch (Exception e)
        {
            Debug.Fail(e.Message);
        }
    }
}
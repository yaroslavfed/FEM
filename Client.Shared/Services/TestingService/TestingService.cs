using System.Diagnostics;
using System.Text.Json;
using Client.Shared.API;
using Client.Shared.API.TestingServiceClient;
using Client.Shared.Data;
using Client.Shared.HttpClientContext;
using Client.Shared.Services.ReportService;

namespace Client.Shared.Services.TestingService;

public class TestingService : ITestingService
{
    private readonly ITestingServiceClient _client;
    private readonly IReportService        _reportService;

    public TestingService(ITestingServiceClient client, IReportService reportService)
    {
        _client = client;
        _reportService = reportService;
    }

    public async Task<FemResponse> CreateSessionAsync(Data.TestSession testSession)
    {
        try
        {
            return await _client.CalculateSolutionVector(testSession);
        } catch (Exception e)
        {
            Debug.Fail(e.Message);
            throw;
        }
    }

    public async Task<TestResult?> GetSessionResultAsync(Guid id)
    {
        TestResult? testResult;
        var path = $"Client/Plots_{id}/";
        try
        {
            var response = await _client.GetSessionResult(id);
            testResult = JsonSerializer.Deserialize<TestResult>(response);

            if (testResult is null)
                return null;

            if (Directory.Exists(path))
                return testResult;

            Directory.CreateDirectory(path);
            foreach (var plot in testResult.Plots.Select((value, index) => new { index, value }))
            {
                var bytes = Convert.FromBase64String(plot.value);
                using FileStream fileStream = new FileStream(
                    Path.Combine(path, $"plot{plot.index}.png"),
                    FileMode.CreateNew
                );
                fileStream.Write(bytes);
            }

            return testResult;
        } catch (Exception e)
        {
            Debug.Fail(e.Message);
            throw;
        }

        return testResult;
    }
}
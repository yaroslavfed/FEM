using Client.Shared.Data;

namespace Client.Shared.Services.ReportService;

public interface IReportService
{
    Task GenerateReportAsync(TestResult testResult);
}
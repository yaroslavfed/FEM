using Client.Shared.Data;

namespace Client.Shared.Services.ReportService;

public interface IReportService
{
    void GenerateReportAsync(TestResult testResult);
}
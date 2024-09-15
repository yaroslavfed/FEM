using FEM.Server.Data;

namespace FEM.Server.Services.TestResultService;

public interface ITestResultService
{
    Task<Guid> AddTestResultAsync(SolutionResult solutionParameters);

    Task<string> GetTestResultAsync(Guid id);
}
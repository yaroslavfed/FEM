using FEM.Server.Data;
using FEM.Server.Data.OutputModels;

namespace FEM.Server.Services.TestResultService;

public interface ITestResultService
{
    Task<Guid> AddTestResultAsync(SolutionResult solutionParameters);

    Task<string> GetTestResultAsync(Guid id);
}
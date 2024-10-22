using FEM.Common.DTO.Models.OutputModels;

namespace FEM.Common.Core.Services.TestResultService;

public interface ITestResultService
{
    Task<Guid> AddTestResultAsync(SolutionResult solutionParameters);

    Task<string> GetTestResultAsync(Guid id);
}
using FEM.SharedDTO.Models.OutputModels;

namespace Stationary.Core.Services.TestResultService;

public interface IStationaryTestResultService
{
    Task<Guid> AddTestResultAsync(SolutionResult solutionParameters);

    Task<string> GetTestResultAsync(Guid id);
}
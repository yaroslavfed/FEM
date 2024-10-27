using FEM.SharedDTO.Abstractions;
using FEM.SharedDTO.Models.OutputModels;

namespace FEM.Core.Services.InaccuracyService;

public interface IInaccuracyService
{
    Task GetSolutionVectorInaccuracy(TestSessionBase testSession, SolutionResult solutionResult);
}
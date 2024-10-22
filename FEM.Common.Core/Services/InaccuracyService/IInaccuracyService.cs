using FEM.Common.DTO.Abstractions;
using FEM.Common.DTO.Models.OutputModels;

namespace FEM.Common.Core.Services.InaccuracyService;

public interface IInaccuracyService
{
    Task GetSolutionVectorInaccuracy(TestSessionBase testSession, SolutionResult solutionResult);
}
using FEM.Common.DTO.Abstractions;
using FEM.Common.DTO.Models.MatrixFormats;
using FEM.Common.DTO.Models.OutputModels;

namespace FEM.Common.Core.Services.SolverService;

public interface ISolverService
{
    Task<SolutionResult> GetSolutionVectorAsync(MatrixProfileFormat matrixFormat, int maxIterationsCount, double eps);
}
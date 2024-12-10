using FEM.SharedDTO.Models.MatrixFormats;
using FEM.SharedDTO.Models.OutputModels;

namespace FEM.SharedCore.Services.SolverService;

public interface ISolverService
{
    Task<SolutionResult> GetSolutionVectorAsync(MatrixProfileFormat matrixFormat, int maxIterationsCount, double eps);
}
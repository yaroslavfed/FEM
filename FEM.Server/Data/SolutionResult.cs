using FEM.Common.Data.MathModels;

namespace FEM.Server.Data;

public record SolutionResult(
    Vector Solve,
    double Discrepancy,
    int ItersCount
);
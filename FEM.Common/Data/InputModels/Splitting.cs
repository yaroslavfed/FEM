using FEM.Shared.Domain.MathModels;

namespace FEM.Common.Data.InputModels;

public abstract record Splitting
{
    public required Point3D StepCount { get; init; }
    
    public required Point3D Kr { get; init; }
}
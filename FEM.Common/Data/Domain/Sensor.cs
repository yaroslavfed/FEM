using FEM.Common.Data.MathModels;
using FEM.Server.Data;

namespace FEM.Common.Data.Domain;

public record Sensor
{
    public required Point3D Position { get; init; }
    
    public required ESensorComponent ComponentIndex { get; init; } // 0=X, 1=Y, 2=Z
}
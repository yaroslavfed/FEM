using FEM.Common.Data.MathModels;
using FEM.Server.Data;

namespace FEM.Common.Data.Domain;

public record Sensor
{
    public required Point3D Position { get; init; }

    public required ESensorComponent ComponentDirection { get; init; }
}
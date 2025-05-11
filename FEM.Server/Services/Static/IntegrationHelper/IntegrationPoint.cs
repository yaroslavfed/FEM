using FEM.Common.Data.MathModels;

namespace FEM.Server.Services.Static.IntegrationHelper;

public record IntegrationPoint
{
    public required Point3D Position { get; init; }

    public required double Weight { get; init; }
}
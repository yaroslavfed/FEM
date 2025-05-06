using FEM.Common.Data.MathModels;

namespace FEM.Server.Data.Domain;

public record CurrentSegment
{
    public Point3D Center { get; init; }
    public Vector3D Direction { get; init; } // Нормированный вектор направления
    public double Current { get; init; }     // Величина тока в сегменте
}
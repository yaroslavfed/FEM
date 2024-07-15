namespace FEM.Common.Data.MathModels;

/// <summary>
/// Координаты в декартовом пространстве
/// </summary>
public record Point3D
{
    /// <summary>
    /// Координата по X
    /// </summary>
    public double X { get; init; }

    /// <summary>
    /// Координата по Y
    /// </summary>
    public double Y { get; init; }

    /// <summary>
    /// Координата по Z
    /// </summary>
    public double Z { get; init; }

    public override string ToString()
    {
        return $"[{X}, {Y}, {Z}]";
    }
}
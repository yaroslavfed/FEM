namespace VectorFEM.Data;

/// <summary>
/// Структура узла трехмерной сетки 
/// </summary>
/// <param name="X">Координата по OX</param>
/// <param name="Y">Координата по OY</param>
/// <param name="Z">Координата по OZ</param>
public record Node
{
    public double X { get; init; }
    public double Y { get; init; }
    public double Z { get; init; }
}
namespace VectorFEM.Data;

/// <summary>
/// Структура узла трехмерной сетки 
/// </summary>
/// <param name="X">Координата по OX</param>
/// <param name="Y">Координата по OY</param>
/// <param name="Z">Координата по OZ</param>
public record Node(
    double X,
    double Y,
    double Z
);
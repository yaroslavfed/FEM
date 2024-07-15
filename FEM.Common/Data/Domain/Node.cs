using FEM.Shared.Domain.MathModels;

namespace FEM.Shared.Domain.Data;

/// <summary>
/// Структура узла трехмерной сетки 
/// </summary>
/// <param name="X">Координата по OX</param>
/// <param name="Y">Координата по OY</param>
/// <param name="Z">Координата по OZ</param>
public record Node
{
    public required Point3D Coordinate { get; init; }
}
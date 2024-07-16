using FEM.Common.Data.MathModels;

namespace FEM.Common.Data.Domain;

/// <summary>
/// Структура узла трехмерной сетки 
/// </summary>
public record Node
{
    public required Point3D Coordinate { get; init; }
}
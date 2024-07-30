﻿using FEM.Common.Data.MathModels;

namespace VectorFEM.Core.Data.Parallelepipedal;

/// <summary>
/// Границы конечного элемента в декартовой системе координат
/// </summary>
public record FiniteElementBounds
{
    /// <summary>
    /// Нижняя точка относительно схемы построения КЭ
    /// </summary>
    public required Point3D LowCoordinate { get; init; }

    /// <summary>
    /// Верхняя точка относительно схемы построения КЭ
    /// </summary>
    public required Point3D HighCoordinate { get; init; }
}
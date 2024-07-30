﻿namespace VectorFEM.Core.Data.Parallelepipedal;

/// <summary>
/// Структура конечного элемента с нумерованными узлами и ребрами
/// </summary>
public record FiniteElementWithNumerics
{
    /// <summary>
    /// Список узлов КЭ
    /// </summary>
    public IList<int> Nodes { get; set; } = [];

    /// <summary>
    /// Список ребер КЭ
    /// </summary>
    public IList<int> Edges { get; set; } = [];

    /// <summary>
    /// Соотношение ребер с их узлами
    /// </summary>
    public IList<(int First, int Second)> MapNodesEdges { get; set; } = [];
}
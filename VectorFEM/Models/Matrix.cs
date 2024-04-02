namespace VectorFEM.Models;

public struct Matrix
{
    public IReadOnlyList<IReadOnlyList<double>> Data { get; set; }
}
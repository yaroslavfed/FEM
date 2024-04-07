using VectorFEM.Data;

namespace VectorFEM.Extensions;

public static class MathExtensions
{
    public static Matrix Transpose(this Matrix matrix) =>
        new()
        {
            Data = matrix.Data.Select(inner => inner.Select((s, i) => new { s, i }))
                .SelectMany(a => a.ToList())
                .GroupBy(a => a.i, a => a.s)
                .Select(a => a.ToList()).ToList()
        };
}
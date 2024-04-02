namespace VectorFEM.Models;

public struct Vector
{
    public IReadOnlyList<double> Data { get; set; }

    public static Vector operator *(Vector vector, double unit) =>
        new()
        {
            Data = vector.Data.Select(item => item * unit).ToList()
        };

    public static Vector operator +(Vector vector1, Vector vector2)
    {
        return vector1.Data.Count == vector2.Data.Count
            ? new Vector
            {
                Data = vector1.Data.Select((i, j) => i + vector2.Data[j]).ToList()
            }
            : throw new IndexOutOfRangeException("Векторы разной размерности");
    }

    public static double operator *(Vector vector1, Vector vector2) =>
        vector1.Data.Select((i, j) => i * vector2.Data[j]).Sum();
}
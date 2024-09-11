using System.Text;

namespace FEM.Common.Data.MathModels;

public record Vector
{
    public List<double> Data { get; init; } = [];

    public Vector()
    {
    }

    public Vector(int size) => Data = [..Enumerable.Repeat(0.0, size)];

    public Vector(List<double> nums) => Data = [..nums];

    public double this[int index]
    {
        get => Data[index];
        set => Data[index] = value;
    }

    public static Vector operator *(Vector vector, double unit) =>
        new() { Data = vector.Data.Select(item => item * unit).ToList() };

    public static Vector operator +(Vector vector1, Vector vector2) =>
        vector1.Data.Count == vector2.Data.Count
            ? new Vector { Data = vector1.Data.Select((i, j) => i + vector2.Data[j]).ToList() }
            : throw new IndexOutOfRangeException("Векторы разной размерности");

    public static double operator *(Vector vector1, Vector vector2) =>
        vector1.Data.Select((i, j) => i * vector2.Data[j]).Sum();

    public static Vector operator *(double coef, Vector vector) => new(vector.Data.Select(num => num * coef).ToList());

    public static Vector operator -(Vector vector1, Vector vector2) =>
        new Vector { Data = vector1.Data.Select((i, j) => i - vector2.Data[j]).ToList() };

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        foreach (var item in Data) stringBuilder.AppendLine($"{item}");
        return stringBuilder.ToString();
    }
}
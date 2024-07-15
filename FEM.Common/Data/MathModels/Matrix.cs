using System.Text;

namespace FEM.Common.Data.MathModels;

public record Matrix
{
    public IReadOnlyList<IReadOnlyList<double>> Data { get; init; } = [];

    public static Matrix operator *(Matrix matrix, double unit) =>
        new()
        {
            Data = matrix.Data.Select(line => line.Select(item => item * unit).ToList()).ToList()
        };

    public static Matrix operator +(Matrix matrix1, Matrix matrix2) =>
        new()
        {
            Data = matrix1.Data
                .Select((t1, i) =>
                    matrix1.Data.Select((_, j) =>
                        t1[j] + matrix2.Data[i][j]).ToList())
                .Cast<IReadOnlyList<double>>().ToList()
        };

    public override string ToString()
    {
        var matrixBuilder = new StringBuilder();
        foreach (var line in Data)
        {
            foreach (var item in line)
            {
                matrixBuilder.Append($"{item:N3} ");
            }

            matrixBuilder.Append('\n');
        }

        return matrixBuilder.ToString();
    }
}
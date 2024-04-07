using System.Text;

namespace VectorFEM.Data;

public record Matrix
{
    public IReadOnlyList<IReadOnlyList<double>> Data { get; init; } = [];

    public static Matrix operator *(Matrix matrix, double unit) =>
        new()
        {
            Data = matrix.Data.Select(line => line.Select(item => item * unit).ToList()).ToList()
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
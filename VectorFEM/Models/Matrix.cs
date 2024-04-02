using System.Text;

namespace VectorFEM.Models;

public struct Matrix
{
    public IReadOnlyList<IReadOnlyList<double>> Data { get; set; }

    public override string ToString()
    {
        var matrixBuilder = new StringBuilder();
        foreach (var line in Data)
        {
            foreach (var item in line)
            {
                matrixBuilder.Append($"{item:N3} ");
            }

            matrixBuilder.Append("\n");
        }

        return matrixBuilder.ToString();
    }
}
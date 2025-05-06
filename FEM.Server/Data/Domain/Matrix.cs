namespace FEM.Server.Data.Domain;

public record Matrix
{
    private readonly double[,] _data;

    public int Rows { get; }

    public int Columns { get; }

    public Matrix(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        _data = new double[rows, columns];
    }

    public double this[int i, int j]
    {
        get => _data[i, j];
        set => _data[i, j] = value;
    }

    public void Add(Matrix other)
    {
        if (other.Rows != Rows || other.Columns != Columns)
            throw new ArgumentException("Matrix dimensions do not match");

        for (int i = 0; i < Rows; i++)
            for (int j = 0; j < Columns; j++)
                _data[i, j] += other[i, j];
    }

    public Vector Multiply(Vector vector)
    {
        if (vector.Size != Columns)
            throw new ArgumentException("Matrix and vector dimensions do not match");

        Vector result = new Vector(Rows);
        for (int i = 0; i < Rows; i++)
        {
            double sum = 0;
            for (int j = 0; j < Columns; j++)
                sum += _data[i, j] * vector[j];

            result[i] = sum;
        }

        return result;
    }

    public override string ToString()
    {
        var lines = new List<string>();
        for (int i = 0; i < Rows; i++)
        {
            var row = Enumerable.Range(0, Columns).Select(j => _data[i, j].ToString("0.###")).ToArray();
            lines.Add("[" + string.Join(", ", row) + "]");
        }

        return string.Join("\n", lines);
    }
}
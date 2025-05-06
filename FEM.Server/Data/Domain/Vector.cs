namespace FEM.Server.Data.Domain;

public record Vector
{
    private readonly double[] _data;

    public int Size => _data.Length;

    public Vector(int size)
    {
        _data = new double[size];
    }

    public Vector(IEnumerable<double> values)
    {
        _data = values.ToArray();
    }

    public double this[int i]
    {
        get => _data[i];
        set => _data[i] = value;
    }

    public void Add(Vector other)
    {
        if (other.Size != Size)
            throw new ArgumentException("Vector sizes do not match");

        for (int i = 0; i < Size; i++)
            _data[i] += other[i];
    }

    public void Scale(double scalar)
    {
        for (int i = 0; i < Size; i++)
            _data[i] *= scalar;
    }

    public double Dot(Vector other)
    {
        if (other.Size != Size)
            throw new ArgumentException("Vector sizes do not match");

        double sum = 0;
        for (int i = 0; i < Size; i++)
            sum += _data[i] * other[i];

        return sum;
    }

    public double Norm() => Math.Sqrt(Dot(this));

    public override string ToString() => "[" + string.Join(", ", _data.Select(x => x.ToString("0.###"))) + "]";
}
namespace FEM.Common.Data.MathModels.MatrixFormats;

/// <summary>
/// Массивы для СЛАУ в профильном формате
/// </summary>
public record MatrixProfileFormat
{
    public List<int> Ig { get; set; } = [];

    public List<int> Jg { get; set; } = [];

    public List<double> Di { get; set; } = [];

    public List<double> Gg { get; set; } = [];

    public List<double> F { get; set; } = [];
}
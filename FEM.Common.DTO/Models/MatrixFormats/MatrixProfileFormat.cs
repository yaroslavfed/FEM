using FEM.Common.DTO.Models.MathModels;

namespace FEM.Common.DTO.Models.MatrixFormats;

/// <summary>
/// Профильный формат хранения матрицы
/// </summary>
public record MatrixProfileFormat
{
    public List<int> Ig { get; private set; } = [];

    public List<int> Jg { get; private set; } = [];

    public List<double> Di { get; private set; } = [];

    public List<double> Gg { get; private set; } = [];

    public List<double> F { get; private set; } = [];

    public int Size => Di.Count;

    public int TriangleSize => Gg.Count;

    /// <summary>
    /// Инициализация базовых векторов 
    /// </summary>
    /// <param name="edgesCount">Количество ребер КЭ</param>
    public Task InitializeVectorsAsync(int edgesCount)
    {
        Di = [.. Enumerable.Range(0, edgesCount).Select(_ => 0)];
        F = [.. Enumerable.Range(0, edgesCount).Select(_ => 0)];

        return Task.CompletedTask;
    }

    /// <summary>
    /// Собираем портрет матрицы
    /// </summary>
    public Task<MatrixProfileFormat> CreateProfileArraysAsync(List<List<int>> positionsList)
    {
        Ig = [0, 0, .. Enumerable.Range(0, positionsList.Count - 1).Select(_ => 0)];
        for (var i = 1; i < positionsList.Count; i++)
            Ig[i + 1] = Ig[i] + positionsList[i].Count;

        Gg = [.. Enumerable.Range(0, Ig.Last()).Select(_ => 0)];
        Jg = [.. Enumerable.Range(0, Ig.Last()).Select(_ => 0)];
        for (int i = 1, j = 0; i < positionsList.Count; i++)
            foreach (var bufferItem in positionsList[i])
                Jg[j++] = bufferItem;

        return Task.FromResult(this);
    }

    /// <summary>
    /// Добавляем вклады каждого КЭ в глобальную матрицу, дописывая значение на главную диагональ и в профиль
    /// </summary>
    public Task AddElementToGlobalMatrixAsync(int i, int j, double element)
    {
        if (i == j)
        {
            Di[i] += element;
            return Task.CompletedTask;
        }

        for (var index = Ig[i]; index < Ig[i + 1]; index++)
            if (Jg[index] == j)
            {
                Gg[index] += element;
                return Task.CompletedTask;
            }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Добавляем вклады каждого КЭ в вектор правой части
    /// </summary>
    public Task AddElementToRightPartAsync(int index, double coefficient)
    {
        F[index] += coefficient;

        return Task.CompletedTask;
    }

    public static Vector operator *(MatrixProfileFormat matrix, Vector vector)
    {
        if (matrix.Size != vector.Data.Count)
            throw new Exception("Sizes of the matrix and vector aren'T equals");

        Vector result = new Vector(vector.Data.Count);

        for (int i = 0; i < vector.Data.Count; i++)
            result[i] = matrix.Di[i] * vector[i];


        for (int i = 0; i < vector.Data.Count; i++)
        {
            for (int j = matrix.Ig[i]; j < matrix.Ig[i + 1]; j++)
            {
                result[i] += matrix.Gg[j] * vector[matrix.Jg[j]];
                result[matrix.Jg[j]] += matrix.Gg[j] * vector[i];
            }
        }

        return result;
    }
}
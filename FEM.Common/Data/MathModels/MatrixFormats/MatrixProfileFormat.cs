namespace FEM.Common.Data.MathModels.MatrixFormats;

/// <summary>
/// Профильный формат хранения матрицы
/// </summary>
public record MatrixProfileFormat : IMatrixFormat
{
    public List<int> Ig { get; private set; } = [];

    public List<int> Jg { get; private set; } = [];

    public List<double> Di { get; private set; } = [];

    public List<double> Gg { get; private set; } = [];

    public List<double> F { get; private set; } = [];

    public Task InitializeVectorsAsync(int edgesCount)
    {
        Di = [..Enumerable.Range(0, edgesCount).Select(_ => 0)];
        F = [..Enumerable.Range(0, edgesCount).Select(_ => 0)];

        return Task.CompletedTask;
    }

    /// <inheritdoc cref="IMatrixFormat.CreateProfileArraysAsync"/>
    public Task<IMatrixFormat> CreateProfileArraysAsync(List<List<int>> positionsList)
    {
        Ig = [0, 0, ..Enumerable.Range(0, positionsList.Count - 1).Select(_ => 0)];
        for (var i = 1; i < positionsList.Count; i++)
            Ig[i + 1] = Ig[i] + positionsList[i].Count;

        Gg = [..Enumerable.Range(0, Ig.Last()).Select(_ => 0)];
        Jg = [..Enumerable.Range(0, Ig.Last()).Select(_ => 0)];
        for (int i = 1, j = 0; i < positionsList.Count; i++)
            foreach (var bufferItem in positionsList[i])
                Jg[j++] = bufferItem;

        return Task.FromResult(this as IMatrixFormat);
    }

    /// <inheritdoc cref="IMatrixFormat.AddElementToGlobalMatrixAsync"/>
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

    /// <inheritdoc cref="IMatrixFormat.AddElementToRightPartAsync"/>
    public Task AddElementToRightPartAsync(int index, double coefficient)
    {
        F[index] += coefficient;

        return Task.CompletedTask;
    }
}
namespace FEM.Common.Data.MathModels.MatrixFormats;

/// <summary>
/// Массивы для СЛАУ в профильном формате
/// </summary>
public record MatrixProfileFormat : IMatrixFormat
{
    public List<int> Ig { get; set; } = [];

    public List<int> Jg { get; set; } = [];

    public List<double> Di { get; set; } = [];

    public List<double> Gg { get; set; } = [];

    public List<double> F { get; set; } = [];

    /// <summary>
    /// Собираем портрет матрицы
    /// </summary>
    public Task<IMatrixFormat> CreateProfileArraysAsync(List<List<int>> positionsList)
    {
        Ig = [0, 0, ..Enumerable.Range(0, positionsList.Count - 1).Select(item => 0)];
        for (var i = 1; i < positionsList.Count; i++)
            Ig[i + 1] = Ig[i] + positionsList[i].Count;

        Jg = [..Enumerable.Range(0, Ig.Last()).Select(item => 0)];
        for (int i = 1, j = 0; i < positionsList.Count; i++)
            foreach (var bufferItem in positionsList[i])
                Jg[j++] = bufferItem;

        return Task.FromResult(this as IMatrixFormat);
    }

    /// <summary>
    /// Добавляем элемент в глобальную матрицу, дописывая значение на главную диагональ и в профиль
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

    public Task AddElementToRightPartAsync(int index, double coefficient)
    {
        F = [..Enumerable.Range(0, 12).Select(item => 0)];
        F[index] += coefficient;

        return Task.CompletedTask;
    }
}
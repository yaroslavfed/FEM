namespace FEM.Common.Data.MathModels.MatrixFormats;

public interface IMatrixFormat
{
    /// <summary>
    /// Собираем портрет матрицы
    /// </summary>
    Task<IMatrixFormat> CreateProfileArraysAsync(List<List<int>> positionsList);

    /// <summary>
    /// Инициализация базовых векторов 
    /// </summary>
    /// <param name="edgesCount">Количество ребер КЭ</param>
    Task InitializeVectorsAsync(int edgesCount);

    /// <summary>
    /// Добавляем вклады каждого КЭ в глобальную матрицу, дописывая значение на главную диагональ и в профиль
    /// </summary>
    Task AddElementToGlobalMatrixAsync(int i, int j, double element);

    /// <summary>
    /// Добавляем вклады каждого КЭ в вектор правой части
    /// </summary>
    Task AddElementToRightPartAsync(int index, double coefficient);
}
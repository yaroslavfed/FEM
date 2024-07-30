using VectorFEM.Core.Data.Parallelepipedal;

namespace VectorFEM.Core.Services.Parallelepipedal.MatrixPortraitService;

/// <summary>
/// Сервис построения профиля матрицы
/// </summary>
/// <typeparam name="TMesh">Тип модели хранения сетки</typeparam>
/// <typeparam name="TMatrixFormat">Тип хранения матрицы</typeparam>
public interface IMatrixPortraitService<TMatrixFormat>
{
    /// <summary>
    /// Получаем портрет матрицы
    /// </summary>
    /// <param name="mesh">Сетка расчётной области</param>
    /// <returns>Получаем матрицу в заданном формате</returns>
    Task<TMatrixFormat> ResolveMatrixPortraitAsync(Mesh mesh);
}
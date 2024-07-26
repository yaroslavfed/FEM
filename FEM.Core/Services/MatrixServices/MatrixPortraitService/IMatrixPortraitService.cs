namespace FEM.Core.Services.MatrixServices.MatrixPortraitService;

/// <summary>
/// Сервис построения профиля матрицы
/// </summary>
/// <typeparam name="TMesh">Тип модели хранения сетки</typeparam>
/// <typeparam name="TMatrixFormat">Тип хранения матрицы</typeparam>
public interface IMatrixPortraitService<in TMesh, TMatrixFormat>
{
    /// <summary>
    /// Получаем портрет матрицы
    /// </summary>
    /// <param name="mesh">Сетка расчётной области</param>
    /// <returns>Получаем матрицу в заданном формате</returns>
    Task<TMatrixFormat> ResolveMatrixPortrait(TMesh mesh);
}
using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Common.Enums;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.Parallelepipedal.MatrixPortraitService;

/// <summary>
/// Сервис построения профиля матрицы
/// </summary>
public interface IMatrixPortraitService
{
    /// <summary>
    /// Получаем портрет матрицы
    /// </summary>
    /// <param name="mesh">Сетка расчётной области</param>
    /// <param name="matrixFormat">Формат хранения матрицы</param>
    /// <returns>Получаем матрицу в указанном формате</returns>
    Task<IMatrixFormat> ResolveMatrixPortraitAsync(Mesh mesh, EMatrixFormats matrixFormat);
}
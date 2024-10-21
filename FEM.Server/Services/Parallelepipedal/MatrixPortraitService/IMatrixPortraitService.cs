using FEM.Common.Data.MatrixFormats;
using FEM.Common.Data.MeshModels;

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
    /// <returns>Получаем матрицу в указанном формате</returns>
    Task<MatrixProfileFormat> ResolveMatrixPortraitAsync(Mesh mesh);
}
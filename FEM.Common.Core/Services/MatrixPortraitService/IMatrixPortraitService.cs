using FEM.Common.DTO.Models.MatrixFormats;
using FEM.Common.DTO.Models.MeshModels;

namespace FEM.Common.Core.Services.MatrixPortraitService;

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
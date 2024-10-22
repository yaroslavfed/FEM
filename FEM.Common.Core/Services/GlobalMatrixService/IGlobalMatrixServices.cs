using FEM.Common.DTO.Abstractions;
using FEM.Common.DTO.Models.MatrixFormats;

namespace FEM.Common.Core.Services.GlobalMatrixService;

/// <summary>
/// Сервис построения глобальной матрицы
/// </summary>
public interface IGlobalMatrixServices
{
    /// <summary>
    /// Получаем глобальную матрицу
    /// </summary>
    /// <param name="matrixProfile"><see cref="MatrixProfileFormat">Формат хранения матрицы</see></param>
    /// <param name="testSession"><see cref="TestSessionBase">Сессия тестирования</see></param>
    Task GetGlobalMatrixAsync(MatrixProfileFormat matrixProfile, TestSessionBase testSession);
}
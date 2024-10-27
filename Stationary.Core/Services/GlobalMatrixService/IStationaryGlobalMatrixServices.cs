using FEM.SharedDTO.Abstractions;
using FEM.SharedDTO.Models.MatrixFormats;

namespace Stationary.Core.Services.GlobalMatrixService;

/// <summary>
/// Сервис построения глобальной матрицы
/// </summary>
public interface IStationaryGlobalMatrixServices
{
    /// <summary>
    /// Получаем глобальную матрицу
    /// </summary>
    /// <param name="matrixProfile"><see cref="MatrixProfileFormat">Формат хранения матрицы</see></param>
    /// <param name="testSession"><see cref="TestSessionBase">Сессия тестирования</see></param>
    Task GetGlobalMatrixAsync(MatrixProfileFormat matrixProfile, TestSessionBase testSession);
}
using FEM.SharedDTO.Abstractions;
using FEM.SharedDTO.Models.MatrixFormats;

namespace Stationary.Core.Services.RightPartVectorService;

/// <summary>
/// Сервис построения вектора правой части
/// </summary>
public interface IStationaryRightPartVectorService
{
    /// <summary>
    /// Получение вектора правой части
    /// </summary>
    /// <param name="matrixProfile"><see cref="MatrixProfileFormat">Формат хранения</see></param>
    /// <param name="testSession"><see cref="TestSessionBase"/></param>
    Task GetRightPartVectorAsync(MatrixProfileFormat matrixProfile, TestSessionBase testSession);
}
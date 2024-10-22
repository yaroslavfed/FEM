using FEM.Common.DTO.Abstractions;
using FEM.Common.DTO.Models.MatrixFormats;

namespace FEM.Common.Core.Services.RightPartVectorService;

/// <summary>
/// Сервис построения вектора правой части
/// </summary>
public interface IRightPartVectorService
{
    /// <summary>
    /// Получение вектора правой части
    /// </summary>
    /// <param name="matrixProfile"><see cref="MatrixProfileFormat">Формат хранения</see></param>
    /// <param name="testSession"><see cref="TestSessionBase"/></param>
    Task GetRightPartVectorAsync(MatrixProfileFormat matrixProfile, TestSessionBase testSession);
}
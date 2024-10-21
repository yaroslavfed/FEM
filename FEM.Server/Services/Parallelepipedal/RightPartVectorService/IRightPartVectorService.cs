using FEM.Common.Data.MeshModels;
using FEM.NonStationary.DTO.TestingContext;

namespace FEM.Server.Services.Parallelepipedal.RightPartVectorService;

/// <summary>
/// Сервис построения вектора правой части
/// </summary>
public interface IRightPartVectorService
{
    /// <summary>
    /// Получение вектора правой части
    /// </summary>
    /// <param name="matrixProfile">Выбранный формат хранения</param>
    /// <param name="nonStationaryTestSession"><see cref="NonStationaryNonStationaryTestSession{TMesh}"/></param>
    Task GetRightPartVectorAsync(IMatrixFormat matrixProfile, NonStationaryTestSession<Mesh> nonStationaryTestSession);
}
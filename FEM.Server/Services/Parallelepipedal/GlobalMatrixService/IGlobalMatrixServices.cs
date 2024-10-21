using FEM.Common.Data.MeshModels;
using FEM.NonStationary.DTO.TestingContext;

namespace FEM.Server.Services.Parallelepipedal.GlobalMatrixService;

public interface IGlobalMatrixServices
{
    /// <summary>
    /// Получаем глобальную матрицу
    /// </summary>
    /// <param name="matrixProfile"><see cref="NonStationaryNonStationaryTestSession{TMesh}"/></param>
    /// <param name="nonStationaryTestSession">Выбранный формат хранения</param>
    Task GetGlobalMatrixAsync(IMatrixFormat matrixProfile, NonStationaryTestSession<Mesh> nonStationaryTestSession);
}
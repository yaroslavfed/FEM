using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Common.Data.TestSession;
using FEM.Core.Data.Parallelepipedal;

namespace FEM.Core.Services.Parallelepipedal.RightPartVectorService;

/// <summary>
/// Сервис построения вектора правой части
/// </summary>
public interface IRightPartVectorService
{
    /// <summary>
    /// Получение вектора правой части
    /// </summary>
    /// <param name="matrixProfile">Выбранный формат хранения</param>
    /// <param name="testSession"><see cref="TestSession{TMesh}"/></param>
    Task GetRightPartVectorAsync(IMatrixFormat matrixProfile, TestSession<Mesh> testSession);
}
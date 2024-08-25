using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Common.Data.TestSession;
using VectorFEM.Core.Data.Parallelepipedal;

namespace VectorFEM.Core.Services.Parallelepipedal.GlobalMatrixService;

public interface IGlobalMatrixServices
{
    /// <summary>
    /// Получаем глобальную матрицу
    /// </summary>
    /// <param name="matrixProfile"><see cref="TestSession{TMesh}"/></param>
    /// <param name="testSession">Выбранный формат хранения</param>
    Task GetGlobalMatrixAsync(IMatrixFormat matrixProfile, TestSession<Mesh> testSession);
}
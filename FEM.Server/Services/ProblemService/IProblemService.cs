using FEM.Common.Data.Domain;
using FEM.Common.Data.TestSession;
using FEM.Common.Enums;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Models.CurrentSource;

namespace FEM.Server.Services.ProblemService;

public interface IProblemService
{
    /// <summary>
    /// Построение локальной матрицы жёсткости конечного элемента, работающего с ротором векторных базисных функций
    /// </summary>
    /// <param name="element">Конечный элемент</param>
    /// <returns>Локальная матрица жёсткости</returns>
    Task<double[,]> BuildElementStiffnessMatrixAsync(FiniteElement element);

    /// <summary>
    /// Построение локального вектора правой части
    /// </summary>
    /// <param name="element">Конечный элемент</param>
    /// <param name="currentSources">Источники тока</param>
    /// <returns>Локальный вектор правой части</returns>
    Task<double[]> BuildElementRightHandVectorAsync(FiniteElement element, IEnumerable<ICurrentSource> currentSources);

    Task<(Node firstNode, Node secondNode, EDirections direction)> ResolveLocalNodes(
        Edge edge,
        TestSession<Mesh> testSession
    );
}
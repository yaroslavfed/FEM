using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using Vector = FEM.Server.Data.Domain.Vector;

namespace FEM.Server.Services.VectorBuilder;

public interface IVectorBuilder
{
    /// <summary>
    /// Собирает локальный вектор правой части для заданного конечного элемента.
    /// Вклад даётся только от тех сегментов тока, которые попадают внутрь элемента.
    /// </summary>
    /// <param name="element">Конечный элемент</param>
    /// <param name="sources">Список токовых сегментов</param>
    Task<Vector> ComputeLocalRightHandSideAsync(FiniteElement element, IEnumerable<CurrentSegment> sources);
}
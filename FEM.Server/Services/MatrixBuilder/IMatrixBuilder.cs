using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.MatrixBuilder;

public interface IMatrixBuilder
{
    /// <summary>
    /// Собирает локальную матрицу жёсткости (rot-rot) для заданного конечного элемента.
    /// </summary>
    /// <param name="element">Конечный элемент</param>
    Task<Matrix> ComputeLocalStiffnessMatrixAsync(FiniteElement element);
}
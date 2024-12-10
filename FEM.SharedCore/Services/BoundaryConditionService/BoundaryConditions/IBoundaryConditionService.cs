using FEM.SharedDTO.Abstractions;
using FEM.SharedDTO.Models.MatrixFormats;

namespace FEM.SharedCore.Services.BoundaryConditionService.BoundaryConditions;

/// <summary>
/// Сервис задания кравых условий
/// </summary>
public interface IBoundaryConditionService
{
    /// <summary>
    /// Задаем краевые условия
    /// </summary>
    /// <param name="testSession">Сессия тестирования</param>
    /// <param name="matrixProfile">Формат матрицы</param>
    /// <returns></returns>
    Task SetBoundaryConditionsAsync(TestSessionBase testSession, MatrixProfileFormat matrixProfile);
}
using FEM.Common.DTO.Abstractions;
using FEM.Common.DTO.Models.MatrixFormats;

namespace FEM.Common.Core.Services.BoundaryConditionService.BoundaryConditions;

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
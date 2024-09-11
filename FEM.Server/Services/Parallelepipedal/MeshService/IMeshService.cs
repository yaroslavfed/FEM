using FEM.Common.Data.Domain;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.Parallelepipedal.MeshService;

/// <summary>
/// Сервис расчёта сетки
/// </summary>
public interface IMeshService
{
    /// <summary>
    /// Генерация сетки расчётной области
    /// </summary>
    /// <returns></returns>
    Task<Mesh> GenerateMeshAsync(Axis axis);

    /// <summary>
    /// Получение входных данных из внутренней памяти
    /// </summary>
    /// <returns>Параметры проводимого теста</returns>
    Task<Axis> GenerateTestConfiguration();

    /// <summary>
    /// Получение входных данных из внешнего источника
    /// </summary>
    /// <remarks>Для использования с API</remarks>
    /// <param name="testSession">Параметры проводимой расчётной сессии</param>
    /// <returns>Параметры проводимого теста</returns>
    Task<Axis> GenerateTestConfiguration(TestSession testSession);
}
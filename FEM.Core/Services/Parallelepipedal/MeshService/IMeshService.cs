using FEM.Common.Data.Domain;
using FEM.Core.Data.Parallelepipedal;

namespace FEM.Core.Services.Parallelepipedal.MeshService;

/// <summary>
/// Сервис расчёта сетки
/// </summary>
public interface IMeshService
{
    /// <summary>
    /// Генерация сетки расчётной области
    /// </summary>
    /// <returns></returns>
    Task<Mesh> GenerateMeshAsync();

    /// <summary>
    /// Получение входных данных - параметров проводимого теста
    /// </summary>
    /// <returns></returns>
    Task<Axis> GenerateTestConfiguration();
}
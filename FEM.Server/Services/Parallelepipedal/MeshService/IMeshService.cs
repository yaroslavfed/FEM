using FEM.Common.Data.Domain;
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
    Task<Mesh> GenerateMeshAsync();

    /// <summary>
    /// Получение входных данных - параметров проводимого теста
    /// </summary>
    /// <returns></returns>
    Task<Axis> GenerateTestConfiguration();
}
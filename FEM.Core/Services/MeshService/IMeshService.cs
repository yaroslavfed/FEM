using FEM.SharedDTO.Configurations.TestConfiguration;
using FEM.SharedDTO.Domain;
using FEM.SharedDTO.Models.MeshModels;

namespace FEM.Core.Services.MeshService;

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
    /// Получение входных данных из внешнего источника
    /// </summary>
    /// <remarks>Для использования с API</remarks>
    /// <param name="testConfiguration">Параметры проводимой расчётной сессии</param>
    /// <returns>Параметры проводимого теста</returns>
    Task<Axis> GenerateTestConfiguration(TestConfigurationBase testConfiguration);
}
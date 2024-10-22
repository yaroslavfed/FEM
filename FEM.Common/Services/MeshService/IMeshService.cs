using FEM.Common.DTO.Configurations.TestConfiguration;
using FEM.Common.DTO.Domain;
using FEM.Common.DTO.Models.MeshModels;

namespace FEM.Common.Services.MeshService;

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
using FEM.SharedDTO.Configurations.TestConfiguration;
using FEM.SharedDTO.Models.MeshModels.MeshParameters;

namespace Stationary.DTO.Configurations;

/// <summary>
/// Параметры тестирования стационарной задачи
/// </summary>
public record StationaryTestConfiguration : TestConfigurationBase
{
    /// <summary>
    /// Дополнительные параметры решения
    /// </summary>
    public required AdditionParameters AdditionParameters { get; init; }
}
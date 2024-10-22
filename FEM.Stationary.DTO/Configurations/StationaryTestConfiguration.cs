using FEM.Common.DTO.Configurations.TestConfiguration;
using FEM.Common.DTO.Models.MeshModels.MeshParameters;

namespace FEM.Stationary.DTO.Configurations;

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
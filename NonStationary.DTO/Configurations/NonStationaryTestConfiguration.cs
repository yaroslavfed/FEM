using FEM.SharedDTO.Configurations.TestConfiguration;
using FEM.SharedDTO.Models.MeshModels.MeshParameters;
using NonStationary.DTO.InputModels;

namespace NonStationary.DTO.Configurations;

/// <summary>
/// Параметры тестирования нестационарной задачи
/// </summary>
public record NonStationaryTestConfiguration : TestConfigurationBase
{
    /// <summary>
    /// Параметры катушек
    /// </summary>
    public required CoilParameters CoilParameters { get; init; }

    /// <summary>
    /// Параметры сетки по времени
    /// </summary>
    public required TimeGridParameters TimeGridParameters { get; init; }
}
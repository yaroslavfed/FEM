using FEM.Common.DTO.Models.MeshModels.MeshParameters;

namespace FEM.Common.DTO.Configurations.TestConfiguration;

/// <summary>
/// Базовая модель конфигурации тестирования
/// </summary>
public abstract record TestConfigurationBase
{
    /// <summary>
    /// Параметры задания координатной сетки расчётной области
    /// </summary>
    public required MeshParameters MeshParameters { get; init; }

    /// <summary>
    /// Параметры разбиения расчётной области
    /// </summary>
    public required SplittingParameters SplittingParameters { get; init; }

    /// <summary>
    /// Номер краевого условия
    /// </summary>
    public int BoundaryCondition { get; init; }
}
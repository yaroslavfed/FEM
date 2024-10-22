using FEM.Common.DTO.Models.MathModels;
using FEM.Common.DTO.TestingContext;

namespace FEM.Common.DTO.Models.OutputModels;

/// <summary>
/// Результат тестирования
/// </summary>
public record SolutionResult
{
    /// <summary>
    /// Вектор решения
    /// </summary>
    public Vector? Solve { get; init; }

    /// <summary>
    /// Дополнительная информация о решении 
    /// </summary>
    public SolutionAdditionalInfo? SolutionInfo { get; set; }

    /// <summary>
    /// Количество итераций решения СЛАУ
    /// </summary>
    public int ItersCount { get; init; }
}
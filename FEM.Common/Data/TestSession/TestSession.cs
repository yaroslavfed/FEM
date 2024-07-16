using FEM.Common.Data.Domain;

namespace FEM.Common.Data.TestSession;

/// <summary>
/// Структура сессии решения прямой задачи
/// </summary>
public record TestSession
{
    /// <summary>
    /// Расчётная область
    /// </summary>
    public required Mesh Mesh { get; init; }

    public int Mu { get; init; }

    public int Gamma { get; init; }

    public required SplittingCoefficient SplittingCoefficient { get; init; }

    public required MultiplyCoefficient MultiplyCoefficient { get; init; }

    /// <summary>
    /// Порядковый номер мат.задачи передаваемой решателю
    /// </summary>
    public int TestFunctionNumber { get; init; }
}
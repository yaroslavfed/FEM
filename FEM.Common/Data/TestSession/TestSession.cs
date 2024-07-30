using FEM.Common.Data.Domain;

namespace FEM.Common.Data.TestSession;

/// <summary>
/// Структура сессии решения прямой задачи
/// </summary>
public record TestSession<TMesh>
{
    /// <summary>
    /// Расчётная область
    /// </summary>
    public required TMesh Mesh { get; init; }

    public int Mu { get; init; }

    public int Gamma { get; init; }

    /// <summary>
    /// Порядковый номер мат.задачи передаваемой решателю
    /// </summary>
    public int TestFunctionNumber { get; init; }
}
using FEM.Common.Data.TestSession;

namespace FEM.Server.Data.InputModels;

/// <summary>
/// Серверная модель сессии тестирования
/// </summary>
public record TestSession
{
    /// <summary>
    /// Id сессии тестирования
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    /// Параметры сессии тестирования
    /// </summary>
    public required TestSessionBase TestSessionParameters { get; init; }
}
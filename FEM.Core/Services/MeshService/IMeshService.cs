using FEM.Common.Data.Domain;

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
    Task<Mesh> GenerateMeshAsync();
}
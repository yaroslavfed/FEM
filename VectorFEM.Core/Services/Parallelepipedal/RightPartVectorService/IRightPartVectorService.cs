using FEM.Common.Data.Domain;
using VectorFEM.Core.Data.Parallelepipedal;

namespace VectorFEM.Core.Services.Parallelepipedal.RightPartVectorService;

/// <summary>
/// Сервис построения вектора правой части
/// </summary>
public interface IRightPartVectorService
{
    /// <summary>
    /// Расчет значений элементов вектора правой части
    /// </summary>
    Task<double> ResolveRightPartValueAsync(Edge edge, Mesh strata);
}
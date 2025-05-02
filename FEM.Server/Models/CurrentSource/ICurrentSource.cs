using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Models.CurrentSource;

public interface ICurrentSource
{
    /// <summary>
    /// Возвращает вклад источника тока в правую часть для заданного КЭ
    /// </summary>
    Task<double[]> GetElementSourceAsync(FiniteElement element);
}
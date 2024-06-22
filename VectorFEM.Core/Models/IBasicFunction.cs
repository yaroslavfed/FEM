using VectorFEM.Shared.Domain;

namespace VectorFEM.Core.Models;

public interface IBasicFunction<out TData>
{
    TData GetBasicFunctions(int? number = null, Sensor? sensor = null);
}
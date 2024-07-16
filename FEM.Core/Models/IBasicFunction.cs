using FEM.Common.Data.Domain;

namespace FEM.Core.Models;

public interface IBasicFunction<out TData>
{
    TData GetBasicFunctions(int? number = null, Sensor? sensor = null);
}
using FEM.Common.Data.Domain;

namespace FEM.Server.Models.Parallelepipedal.BasicFunction;

public interface IBasicFunction<out TData>
{
    TData GetBasicFunctions(int? number = null, Sensor? sensor = null);
}
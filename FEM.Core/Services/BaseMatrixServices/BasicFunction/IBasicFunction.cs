using FEM.SharedDTO.Domain;

namespace FEM.Core.Services.BaseMatrixServices.BasicFunction;

public interface IBasicFunction<out TData>
{
    TData GetBasicFunctions(int? number = null, Sensor? sensor = null);
}
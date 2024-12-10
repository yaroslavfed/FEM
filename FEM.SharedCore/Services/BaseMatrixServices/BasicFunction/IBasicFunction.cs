using FEM.SharedDTO.Domain;

namespace FEM.SharedCore.Services.BaseMatrixServices.BasicFunction;

public interface IBasicFunction<out TData>
{
    TData GetBasicFunctions(int? number = null, Sensor? sensor = null);
}
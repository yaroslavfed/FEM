using FEM.Common.DTO.Domain;

namespace FEM.Common.Core.Services.BaseMatrixServices.BasicFunction;

public interface IBasicFunction<out TData>
{
    TData GetBasicFunctions(int? number = null, Sensor? sensor = null);
}
namespace VectorFEM.Data;

public interface IBasicFunction<out TData>
{
    TData GetBasicFunctions(FiniteElement element, int? number = null, Sensor? sensor = null);
}
namespace FEM.Storage.Converter;

public interface IConverterService
{
    Task<TOutput> ConvertTo<TInput, TOutput>(TInput input);
}
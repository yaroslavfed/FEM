namespace FEM.Storage.Converter;

public interface IConverter
{
    Task<TOutput> ConvertTo<TInput, TOutput>(TInput input);
}
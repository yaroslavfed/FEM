using System.Text.Json;
using FEM.Common.Parsers;

namespace FEM.Storage.FileStorage.JsonStorage;

/// <summary>
/// Парсер сырых моделей формата JSON
/// </summary>
public class JsonParser : IParser
{
    /// <inheritdoc cref="IParser.DeserializeOutputAsync{TEntity,TData}"/>
    public Task<TEntity> DeserializeOutputAsync<TEntity, TData>(TData context)
    {
        if (context is not FileStream openStream)
            throw new ArgumentException(
                $"Input generic type must be string, but that type is {context?.GetType()}");

        var result = JsonSerializer.Deserialize<TEntity>(openStream) ??
                     throw new ArgumentNullException(
                         $"Returned model must be not null\n{context.GetType()} context is {context}");

        return Task.FromResult<TEntity>(result);
    }

    /// <inheritdoc cref="IParser.ParseEntityFromFileAsync{TEntity}"/>
    public async Task<TEntity> ParseEntityFromFileAsync<TEntity>(string path)
    {
        await using FileStream openStream = File.OpenRead(path);
        return await DeserializeOutputAsync<TEntity, FileStream>(openStream);
    }

    /// <inheritdoc cref="IParser.SerializeInputAsync{TEntity}"/>
    public Task<string> SerializeInputAsync<TEntity>(TEntity @object)
    {
        throw new NotImplementedException();
    }
}
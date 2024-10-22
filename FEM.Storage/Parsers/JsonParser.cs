using System.Text.Json;

namespace FEM.Storage.Parsers;

/// <summary>
/// Парсер сырых моделей формата JSON
/// </summary>
public class JsonParser : IParser
{
    /// <inheritdoc cref="IParser.DeserializeAsync{TEntity,TData}"/>
    public Task<TEntity> DeserializeAsync<TEntity, TData>(TData context)
    {
        if (context is not FileStream openStream)
            throw new ArgumentException($"Input generic type must be string, but that type is {context?.GetType()}");

        var result = JsonSerializer.Deserialize<TEntity>(openStream)
                     ?? throw new ArgumentNullException(
                         $"Returned model must be not null\n{context.GetType()} context is {context}"
                     );

        return Task.FromResult<TEntity>(result);
    }

    /// <inheritdoc cref="IParser.ParseEntityFromFileAsync{TEntity}"/>
    public async Task<TEntity> ParseEntityFromFileAsync<TEntity>(string path)
    {
        await using var openStream = File.OpenRead(path);
        return await DeserializeAsync<TEntity, FileStream>(openStream);
    }

    /// <inheritdoc cref="IParser.SerializeAsync{TEntity}"/>
    public Task<string> SerializeAsync<TEntity>(TEntity @object) => Task.FromResult(JsonSerializer.Serialize(@object));

    public async Task ParseEntityToFileAsync<TEntity>(TEntity context, string fileName)
    {
        var resultFromJson = await SerializeAsync(context);
        await using var streamWriter = new StreamWriter(fileName);
        await streamWriter.WriteLineAsync(resultFromJson);
    }
}
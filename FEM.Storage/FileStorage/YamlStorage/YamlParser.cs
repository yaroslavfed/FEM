using System.Text;
using FEM.Common.Parsers;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FEM.Storage.FileStorage.YamlStorage;

/// <summary>
/// Парсер сырых моделей формата YAML
/// </summary>
internal class YamlParser : IParser
{
    /// <inheritdoc cref="IParser.DeserializeOutputAsync{TEntity,TData}"/>
    public Task<TEntity> DeserializeOutputAsync<TEntity, TData>(TData context)
    {
        if (context is not string nonDeserializedLine)
            throw new ArgumentException($"Input generic type must be string, but that type is {context?.GetType()}");

        var deserializer = new DeserializerBuilder()
                           .WithNamingConvention(UnderscoredNamingConvention.Instance)
                           .Build();

        var result = deserializer.Deserialize<TEntity>(nonDeserializedLine);
        return Task.FromResult(result);
    }

    /// <inheritdoc cref="IParser.SerializeInputAsync{TEntity}"/>
    public Task<string> SerializeInputAsync<TEntity>(TEntity @object)
    {
        var serializer = new SerializerBuilder()
                         .WithNamingConvention(PascalCaseNamingConvention.Instance)
                         .Build();
        var result = serializer.Serialize(@object);

        return Task.FromResult(result);
    }

    /// <inheritdoc cref="IParser.ParseEntityFromFileAsync{TEntity}"/>
    public async Task<TEntity> ParseEntityFromFileAsync<TEntity>(string path)
    {
        using var streamReader = new StreamReader(path, Encoding.UTF8);
        var inputData = await streamReader.ReadToEndAsync();
        return await DeserializeOutputAsync<TEntity, string>(inputData);
    }
}
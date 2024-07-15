using System.Text;
using FEM.Common.Parsers;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FEM.Storage.FileStorage.YamlStorage;

internal class YamlParser : IParser
{
    public Task<TEntity> DeserializeOutput<TEntity>(string nonDeserializedLine)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();

        var result = deserializer.Deserialize<TEntity>(nonDeserializedLine);
        return Task.FromResult(result);
    }

    public Task<string> SerializeInput<TEntity>(TEntity @object)
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();
        var result = serializer.Serialize(@object);

        return Task.FromResult(result);
    }

    public async Task<TEntity> ParseEntityFromFile<TEntity>(string path)
    {
        using var streamReader = new StreamReader(path, Encoding.UTF8);
        var inputData = await streamReader.ReadToEndAsync();
        return await DeserializeOutput<TEntity>(inputData);
    }
}
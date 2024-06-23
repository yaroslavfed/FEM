using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace VectorFEM.Common.Parsers;

public class ParserYaml : IParser
{
    public Task<TEntity> DeserializeOutput<TEntity>(string nonDeserializedLine)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        var result = deserializer.Deserialize<TEntity>(nonDeserializedLine);
        return Task.FromResult(result);
    }

    public Task<string> SerializeInput<TEntity>(TEntity @object)
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
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
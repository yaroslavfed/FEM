using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace VectorFEM.GridBuilder.Parsers;

public class ParserYaml<TEntity> : IParser<TEntity>
{
    public Task<TEntity> DeserializeOutput(string nonDeserializedLine)
    {
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();

        var result = deserializer.Deserialize<TEntity>(nonDeserializedLine);
        return Task.FromResult(result);
    }

    public Task<string> SerializeInput(TEntity @object)
    {
        var serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        var result = serializer.Serialize(@object);

        return Task.FromResult<string>(result);
    }
}
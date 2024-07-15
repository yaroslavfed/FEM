using System.Text;
using FEM.Common.Parsers;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FEM.Storage.FileStorage.YamlStorage;

internal class YamlParser : IParser
{
    public Task<TEntity> DeserializeOutput<TEntity>(string nonDeserializedLine)
    {
        IDeserializer deserializer;
        try
        {
            deserializer = new DeserializerBuilder()
                .WithNamingConvention(UnderscoredNamingConvention.Instance)
                .Build();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }

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
        try
        {
            using var streamReader = new StreamReader(path, Encoding.UTF8);
            var inputData = await streamReader.ReadToEndAsync();
            var result = DeserializeOutput<TEntity>(inputData);
            return await result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }
}
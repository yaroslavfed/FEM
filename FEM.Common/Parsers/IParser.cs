namespace FEM.Common.Parsers;

public interface IParser
{
    Task<TEntity> DeserializeOutput<TEntity>(string nonDeserializedLine);

    Task<TEntity> ParseEntityFromFile<TEntity>(string path);

    Task<string> SerializeInput<TEntity>(TEntity @object);
}
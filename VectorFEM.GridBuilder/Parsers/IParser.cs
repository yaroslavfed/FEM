namespace VectorFEM.GridBuilder.Parsers;

public interface IParser
{
    Task<TEntity> DeserializeOutput<TEntity>(string nonDeserializedLine);

    Task<string> SerializeInput<TEntity>(TEntity @object);
}
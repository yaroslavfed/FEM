namespace VectorFEM.GridBuilder.Parsers;

public interface IParser<TEntity>
{
    Task<TEntity> DeserializeOutput(string nonDeserializedLine);

    Task<string> SerializeInput(TEntity @object);
}
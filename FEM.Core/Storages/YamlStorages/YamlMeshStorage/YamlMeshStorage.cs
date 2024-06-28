using FEM.Common.Parsers;
using FEM.Shared.Domain.YamlModels.YamlMeshBuilder;

namespace FEM.Core.Storages.YamlStorages.YamlMeshStorage;

public class YamlMeshStorage : IReadable<YamlMesh>
{
    private readonly IParser _parser;

    public YamlMeshStorage(IParser parser)
    {
        _parser = parser;
    }

    public async Task<YamlMesh> GetItemAsync()
    {
        return await ReadMeshConfigurationFromFile();
    }

    public Task<IReadOnlyList<YamlMesh>> ListItemsAsync()
    {
        throw new NotImplementedException();
    }

    private async Task<YamlMesh> ReadMeshConfigurationFromFile()
    {
        return await new YamlMeshParser().ParseMesh(_parser);
    }
}
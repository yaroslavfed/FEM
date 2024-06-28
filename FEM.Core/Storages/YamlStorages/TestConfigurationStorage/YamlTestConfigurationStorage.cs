using FEM.Common.Parsers;
using FEM.Shared.Domain.YamlModels.YamlMeshBuilder;

namespace FEM.Core.Storages.YamlStorages.TestConfigurationStorage;

public class YamlTestConfigurationStorage : IReadable<YamlTestSettings>
{
    private readonly IParser _parser;

    public YamlTestConfigurationStorage(IParser parser)
    {
        _parser = parser;
    }

    public async Task<YamlTestSettings> GetItemAsync()
    {
        return await ReadTestConfigurationFromFile();
    }

    public Task<IReadOnlyList<YamlTestSettings>> ListItemsAsync()
    {
        throw new NotImplementedException();
    }

    private async Task<YamlTestSettings> ReadTestConfigurationFromFile()
    {
        return await new YamlMeshParser().ParseTestSettings(_parser);
    }
}
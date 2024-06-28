using System.Collections.Frozen;
using FEM.Common.Parsers;

namespace FEM.Shared.Domain.YamlModels.YamlMeshBuilder;

public class YamlMeshParser
{
    private readonly FrozenDictionary<string, string> _directories = new KeyValuePair<string, string>[]
    {
        new("positioning", Path.Combine(Directory.GetCurrentDirectory(), "positioning.yaml")),
        new("splitting", Path.Combine(Directory.GetCurrentDirectory(), "splitting.yaml")),
        new("additionalParameters", Path.Combine(Directory.GetCurrentDirectory(), "additional_parameters.yaml")),
        new("testingSettings", Path.Combine(Directory.GetCurrentDirectory(), "testing_settings.yaml"))
    }.ToFrozenDictionary();

    public async Task<YamlMesh> ParseMesh(IParser parser)
    {
        return new()
        {
            Positioning
                = await parser.ParseEntityFromFile<Positioning>(_directories["positioning"]),
            Splitting
                = await parser.ParseEntityFromFile<Splitting>(_directories["splitting"])
        };
    }

    public async Task<YamlTestSettings> ParseTestSettings(IParser parser)
    {
        return new()
        {
            AdditionalParameters
                = await parser.ParseEntityFromFile<AdditionalParameters>(_directories["additionalParameters"]),
            TestingSettings
                = await parser.ParseEntityFromFile<TestingSettings>(_directories["testingSettings"])
        };
    }
}
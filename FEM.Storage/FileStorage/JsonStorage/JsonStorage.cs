using System.Collections.Frozen;
using FEM.Common.Data.Domain;
using FEM.Common.Data.InputModels;
using FEM.Common.Parsers;

namespace FEM.Storage.FileStorage.JsonStorage;

public class JsonStorage : IReadableStorage
{
    private readonly IParser _parser;

    private readonly FrozenDictionary<string, string> _directories = new KeyValuePair<string, string>[]
    {
        new("positioning", "Json/positioning.json"),
        new("splitting", "Json/splitting.json"),
        new("additionalParameters", "Json/additional_parameters.json"),
        new("testingSettings", "Json/testing_settings.json")
    }.ToFrozenDictionary();

    public JsonStorage(IParser parser)
    {
        _parser = parser;
    }


    public async Task<Axis> GetAxisAsync()
    {
        return await ReadConfigurationFromFile();
    }

    private async Task<Axis> ReadConfigurationFromFile()
    {
        var positioning = await _parser.ParseEntityFromFileAsync<Positioning>(_directories["positioning"]);
        var splitting = await _parser.ParseEntityFromFileAsync<Splitting>(_directories["splitting"]);
        var additionalParameters
            = await _parser.ParseEntityFromFileAsync<AdditionalParameters>(_directories["additionalParameters"]);
        var testingSettings = await _parser.ParseEntityFromFileAsync<TestingSettings>(_directories["testingSettings"]);

        return new()
        {
            Positioning = positioning,
            Splitting = splitting,
            AdditionalParameters = additionalParameters,
            TestingSettings = testingSettings
        };
    }
}
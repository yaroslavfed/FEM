using System.Collections.Frozen;
using FEM.Common.Data.Domain;
using FEM.Common.Parsers;
using FEM.Storage.Converter;
using FEM.Storage.FileStorage.YamlStorage.Data;

namespace FEM.Storage.FileStorage.YamlStorage;

internal class YamlStorage : IReadableStorage
{
    private readonly IParser    _parser;
    private readonly IConverter _converter;

    private readonly FrozenDictionary<string, string> _directories = new KeyValuePair<string, string>[]
    {
        new("positioning", "Yaml/positioning.yaml"),
        new("splitting", "Yaml/splitting.yaml"),
        new("additionalParameters", "Yaml/additional_parameters.yaml"),
        new("testingSettings", "Yaml/testing_settings.yaml")
    }.ToFrozenDictionary();

    public YamlStorage(IParser parser, IConverter converter)
    {
        _parser = parser;
        _converter = converter;
    }

    public async Task<Axis> GetAxisAsync() => await ReadConfigurationFromFile();

    private async Task<Axis> ReadConfigurationFromFile()
    {
        var positioning = await _parser.ParseEntityFromFileAsync<Positioning>(_directories["positioning"]);
        var splitting = await _parser.ParseEntityFromFileAsync<Splitting>(_directories["splitting"]);
        var additionalParameters
            = await _parser.ParseEntityFromFileAsync<AdditionalParameters>(_directories["additionalParameters"]);
        var testingSettings = await _parser.ParseEntityFromFileAsync<TestingSettings>(_directories["testingSettings"]);

        return new()
        {
            Positioning =
                await _converter.ConvertTo<Positioning, Common.Data.InputModels.Positioning>(positioning),
            Splitting =
                await _converter.ConvertTo<Splitting, Common.Data.InputModels.Splitting>(splitting),
            AdditionalParameters =
                await _converter.ConvertTo<AdditionalParameters, Common.Data.InputModels.AdditionalParameters>(
                    additionalParameters
                ),
            TestingSettings =
                await _converter.ConvertTo<TestingSettings, Common.Data.InputModels.TestingSettings>(testingSettings)
        };
    }
}
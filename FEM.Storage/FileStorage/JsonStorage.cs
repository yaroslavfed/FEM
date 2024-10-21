using System.Collections.Frozen;
using FEM.Common.Data.Domain;
using FEM.Common.Data.InputModels;
using FEM.Common.Parsers;

namespace FEM.Storage.FileStorage;

public class JsonStorage : IJsonStorage
{
    private readonly IParser _parser;

    private readonly FrozenDictionary<string, string> _directories = new KeyValuePair<string, string>[]
    {
        new("positioning", "Json/positioning.json"),
        new("splitting", "Json/splitting.json"),
        new("additionalParameters", "Json/additional_parameters.json")
    }.ToFrozenDictionary();

    public JsonStorage(IParser parser)
    {
        _parser = parser;
    }


    public async Task<Axis> GetAxisAsync() => await ReadConfigurationFromFileAsync();
    
    public async Task SaveResultToFileAsync(TestResult result, string fileName)
    {
        await _parser.ParseEntityToFileAsync(result, fileName);
    }

    private async Task<Axis> ReadConfigurationFromFileAsync()
    {
        var positioning = await _parser.ParseEntityFromFileAsync<Positioning>(_directories["positioning"]);
        var splitting = await _parser.ParseEntityFromFileAsync<Splitting>(_directories["splitting"]);
        var additionalParameters
            = await _parser.ParseEntityFromFileAsync<AdditionalParameters>(_directories["additionalParameters"]);

        return new() { Positioning = positioning, Splitting = splitting, AdditionalParameters = additionalParameters };
    }
}
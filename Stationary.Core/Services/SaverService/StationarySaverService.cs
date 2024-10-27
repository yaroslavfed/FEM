using FEM.SharedDTO.Abstractions;
using FEM.Storage.FileStorage;
using Stationary.DTO.TestingContext;

namespace Stationary.Core.Services.SaverService;

public class StationarySaverService : IStationarySaverService
{
    private readonly IJsonStorage _jsonStorage;

    public StationarySaverService(IJsonStorage jsonStorage)
    {
        _jsonStorage = jsonStorage;
    }

    // TODO: изменить на сохранение в бд
    public async Task SaveResultAsync(StationaryTestResult result)
    {
        Directory.CreateDirectory("TestResults");
        var fileName = $"{result.Id}.txt";
        var path = Path.Combine("TestResults", fileName);
        await _jsonStorage.SaveResultToFileAsync(result, path);
    }

    public async Task WriteListToFileAsync<T>(string fileName, IList<T> list)
    {
        await using var streamWriter = new StreamWriter(fileName);
        foreach (var item in list)
        {
            switch (item)
            {
                case double:
                    await streamWriter.WriteLineAsync($"{item:0.0000E+00}");
                    break;
                case int:
                    await streamWriter.WriteLineAsync($"{item}");
                    break;
            }
        }
    }
}
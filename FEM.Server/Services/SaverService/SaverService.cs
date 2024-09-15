using FEM.Common.Data.TestSession;
using FEM.Storage.FileStorage;

namespace FEM.Server.Services.SaverService;

public class SaverService : ISaverService
{
    private readonly IJsonStorage _jsonStorage;

    public SaverService(IJsonStorage jsonStorage)
    {
        _jsonStorage = jsonStorage;
    }

    // TODO: изменить на сохранение в бд
    public async Task SaveResultAsync(TestResult result)
    {
        Directory.CreateDirectory("TestResults");
        var fileName = $"{result.Id}";
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
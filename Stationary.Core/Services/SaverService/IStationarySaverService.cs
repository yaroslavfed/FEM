using FEM.SharedDTO.Abstractions;
using Stationary.DTO.TestingContext;

namespace Stationary.Core.Services.SaverService;

/// <summary>
/// Сервис сохранения результа тестирования
/// </summary>
public interface IStationarySaverService
{
    /// <summary>
    /// Сохранение результата
    /// </summary>
    /// <param name="result">Результат сессии тестирования</param>
    Task SaveResultAsync(StationaryTestResult result);

    /// <summary>
    /// Запись в файл данных о результате тестирования
    /// </summary>
    /// <param name="fileName">Имя файла для записи</param>
    Task WriteListToFileAsync<T>(string fileName, IList<T> list);
}
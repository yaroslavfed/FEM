using FEM.Common.DTO.Abstractions;

namespace FEM.Common.Services.SaverService;

/// <summary>
/// Сервис сохранения результа тестирования
/// </summary>
public interface ISaverService
{
    /// <summary>
    /// Сохранение результата
    /// </summary>
    /// <param name="result">Результат сессии тестирования</param>
    Task SaveResultAsync(TestResultBase result);

    /// <summary>
    /// Запись в файл данных о результате тестирования
    /// </summary>
    /// <param name="fileName">Имя файла для записи</param>
    Task WriteListToFileAsync<T>(string fileName, IList<T> list);
}
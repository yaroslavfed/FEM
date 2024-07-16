namespace FEM.Common.Parsers;

/// <summary>
/// Парсер сырых данных в модели и обратно
/// </summary>
public interface IParser
{
    /// <summary>
    /// Десериализация сырой строки в модель
    /// </summary>
    /// <param name="context">Сырая строка</param>
    /// <typeparam name="TEntity">Тип модели для десериалицаии</typeparam>
    /// <typeparam name="TData">Тип упаковки сырой строки</typeparam>
    /// <returns>Возвращается модель со свойствами сырой модели</returns>
    Task<TEntity> DeserializeOutputAsync<TEntity, TData>(TData context);

    /// <summary>
    /// Десериализация моделей из файла
    /// </summary>
    /// <param name="path">Путь до файла с сырыми моделями</param>
    /// <typeparam name="TEntity">Модель для десериализации</typeparam>
    /// <returns>Возвращается модель со свойствами модели из файла</returns>
    Task<TEntity> ParseEntityFromFileAsync<TEntity>(string path);

    /// <summary>
    /// Сериализация модели в сырую строку 
    /// </summary>
    /// <param name="object">Модель для сериализации</param>
    /// <typeparam name="TEntity">Тип модели для сериализации</typeparam>
    /// <returns>Возвращается сыроая строка</returns>
    Task<string> SerializeInputAsync<TEntity>(TEntity @object);
}
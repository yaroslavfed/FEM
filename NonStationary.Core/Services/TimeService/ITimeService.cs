using NonStationary.DTO.InputModels;

namespace NonStationary.Core.Services.TimeService;

/// <summary>
/// Сервис работы с временем для нестаионарной задачи
/// </summary>
public interface ITimeService
{
    /// <summary>
    /// Получение сетки по времени из параметров настройки
    /// </summary>
    /// <param name="parameters">Параметры настройки сетки по времени</param>
    /// <returns>Список точек временной сетки</returns>
    Task<List<double>> GetTimeGridParametersAsync(TimeGridParameters parameters);
}
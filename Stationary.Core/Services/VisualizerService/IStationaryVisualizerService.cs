using FEM.SharedDTO.Models.MatrixFormats;
using FEM.SharedDTO.Models.MeshModels;

namespace Stationary.Core.Services.VisualizerService;

/// <summary>
/// Сервис визуализации данных
/// </summary>
public interface IStationaryVisualizerService
{
    /// <summary>
    /// Отрисовка графиков сетки расчётной области
    /// </summary>
    /// <param name="mesh">Сетка расчётной области</param>
    /// <exception cref="FileNotFoundException">Файл с данными о сетке не найден</exception>
    Task DrawMeshPlotAsync(Mesh mesh);

    /// <summary>
    /// Запись компонентов СЛАУ в файд
    /// </summary>
    /// <param name="matrixProfile"></param>
    /// <returns></returns>
    Task WriteMatrixToFileAsync(MatrixProfileFormat matrixProfile);
}
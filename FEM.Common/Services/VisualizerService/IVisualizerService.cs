using FEM.Common.DTO.Models.MatrixFormats;
using FEM.Common.DTO.Models.MeshModels;

namespace FEM.Common.Services.VisualizerService;

/// <summary>
/// Сервис визуализации данных
/// </summary>
public interface IVisualizerService
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
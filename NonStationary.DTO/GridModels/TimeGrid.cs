namespace NonStationary.DTO.GridModels;

/// <summary>
/// Модель времени для нестационарной задачи
/// </summary>
public record TimeGrid
{
    /// <summary>
    /// Сетка по времени
    /// </summary>
    public List<double> GridList { get; init; }
}
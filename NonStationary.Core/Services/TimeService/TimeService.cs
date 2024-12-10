using NonStationary.DTO.InputModels;

namespace NonStationary.Core.Services.TimeService;

/// <inheritdoc cref="ITimeService"/>
public class TimeService : ITimeService
{
    public Task<List<double>> GetTimeGridParametersAsync(TimeGridParameters parameters)
    {
        List<double> timeGrid;
        if (parameters.TimeDecreasingPoint is 0)
        {
            // Изменить параметры для того чтобы создать растянутую вложенную сетку (между каждым предыдущим шагом был еще один)
            for (int i = 0; i < parameters.TimeGridNested; i++)
            {
                parameters.TimeGridSplittingCoefficient /= 1 + Math.Sqrt(parameters.TimeGridMultiplyCoefficient);
                parameters.TimeGridMultiplyCoefficient = Math.Sqrt(parameters.TimeGridMultiplyCoefficient);
            }

            // Количество точек
            var pointsCount = parameters.TimeGridMultiplyCoefficient == 1
                ? (int)Math.Ceiling(
                    (parameters.TimeGridBounds[1] - parameters.TimeGridBounds[0])
                    / parameters.TimeGridSplittingCoefficient
                    + 1
                )
                : (int)(Math.Log(
                            1
                            - (parameters.TimeGridBounds[1] - parameters.TimeGridBounds[0])
                            * (parameters.TimeGridMultiplyCoefficient - 1)
                            / (parameters.TimeGridSplittingCoefficient * (-1))
                        )
                        / Math.Log(parameters.TimeGridMultiplyCoefficient)
                        + 2);

            var h = parameters.TimeGridSplittingCoefficient;
            timeGrid = Enumerable.Repeat(0.0, pointsCount).ToList();
            timeGrid[0] = parameters.TimeGridBounds[0];
            timeGrid[pointsCount - 1] = parameters.TimeGridBounds[1];

            for (var i = 1; i < pointsCount - 1; i++, h *= parameters.TimeGridMultiplyCoefficient)
                timeGrid[i] = timeGrid[i - 1] + h;
        }
        // Ток линейно убывает до точки "parameters.TimeDecreasingPoint"
        else
        {
            parameters.TimeGridSplittingCoefficient
                = parameters.TimeDecreasingPoint / parameters.TimeDecreasingSplitting; // Шаг между 0 и точкой
            // Изменить параметры для того чтобы создать растянутую вложенную сетку (между каждым предыдущим шагом был еще один)
            for (var i = 0; i < parameters.TimeGridNested; i++)
            {
                parameters.TimeGridSplittingCoefficient = (parameters.TimeGridSplittingCoefficient)
                                                          / (1 + Math.Sqrt(parameters.TimeGridMultiplyCoefficient));
                parameters.TimeGridMultiplyCoefficient = Math.Sqrt(parameters.TimeGridMultiplyCoefficient);
            }

            // Количество точек
            var pointsCount = parameters.TimeGridMultiplyCoefficient == 1
                ? (int)Math.Ceiling(
                    ((parameters.TimeGridBounds[1] - parameters.TimeDecreasingPoint)
                     / parameters.TimeGridSplittingCoefficient
                     + 1)
                )
                : (int)(Math.Log(
                            1
                            - (parameters.TimeGridBounds[1] - parameters.TimeDecreasingPoint)
                            * (parameters.TimeGridMultiplyCoefficient - 1)
                            / (parameters.TimeGridSplittingCoefficient * (-1))
                        )
                        / Math.Log(parameters.TimeGridMultiplyCoefficient)
                        + 2);

            var h = parameters.TimeGridSplittingCoefficient;
            timeGrid = new(pointsCount)
            {
                [0] = parameters.TimeDecreasingPoint, [pointsCount - 1] = parameters.TimeGridBounds[1]
            };
            for (var i = 1; i < pointsCount - 1; i++, h *= parameters.TimeGridMultiplyCoefficient)
                timeGrid[i] = timeGrid[i - 1] + h;

            var tmpTimeGrid = new List<double>();
            parameters.TimeGridSplittingCoefficient
                = parameters.TimeDecreasingPoint / parameters.TimeDecreasingSplitting; // Шаг между 0 и точкой
            var timeHInArea = parameters.TimeGridSplittingCoefficient;
            for (var i = 0; i < parameters.TimeGridNested; i++)
                timeHInArea /= 2.0;

            var tmpCoord = parameters.TimeGridBounds[0];
            while (tmpCoord < parameters.TimeDecreasingPoint)
            {
                tmpTimeGrid.Add(tmpCoord);
                tmpCoord += timeHInArea;
            }

            tmpTimeGrid.AddRange(timeGrid);

            timeGrid = new(tmpTimeGrid.Count);
            timeGrid.AddRange(tmpTimeGrid);
        }

        return Task.FromResult(timeGrid);
    }
}
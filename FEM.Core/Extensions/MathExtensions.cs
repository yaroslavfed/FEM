using FEM.Core.Enums;
using FEM.Shared.Domain.MathModels;

namespace FEM.Core.Extensions;

public static class MathExtensions
{
    public static Matrix Transpose(this Matrix matrix) =>
        new()
        {
            Data = matrix.Data.Select(inner => inner.Select((s, i) => new { s, i }))
                .SelectMany(a => a.ToList())
                .GroupBy(a => a.i, a => a.s)
                .Select(a => a.ToList()).ToList()
        };

    public static TResult GetBoundsPoint<TSource, TResult>(
        this IEnumerable<TSource> elements,
        Func<TSource, TResult> selector,
        EPosition position
    )
    {
        var coordinates = elements.Select(selector).ToList();
        coordinates.Sort();

        return position switch
        {
            EPosition.First => coordinates.First(),
            EPosition.Last => coordinates.Last(),
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
    }

    public static List<double> SplitAxis(
        this List<double> axis,
        double multiplyCoefficient,
        int splittingCoefficient,
        double strataLastPoint,
        double strataFirstPoint
    )
    {
        // Обработка по X
        var h = multiplyCoefficient is not 1.0
            ? (strataLastPoint - strataFirstPoint)
              * (1.0 - multiplyCoefficient)
              / (1.0 - Math.Pow(multiplyCoefficient, splittingCoefficient))
            : (strataLastPoint - strataFirstPoint)
              / splittingCoefficient;

        axis.Add(strataFirstPoint);
        for (var i = 0; i < splittingCoefficient; i++)
        {
            axis.Add(axis.Last() + h);
            h *= multiplyCoefficient;
        }

        return axis;
    }
}
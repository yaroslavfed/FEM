using FEM.Common.Data.InputModels;
using FEM.Common.Data.MathModels;
using FEM.Common.Enums;

namespace FEM.Common.Extensions;

public static class MathExtensions
{
    public static Matrix Transpose(this Matrix matrix) =>
        new()
        {
            Data = matrix.Data.Select(inner => inner.Select((s, i) => new { s, i }))
                         .SelectMany(a => a.ToList())
                         .GroupBy(a => a.i, a => a.s)
                         .Select(a => a.ToList())
                         .ToList()
        };

    public static TResult GetBoundsPoint<TSource, TResult>(
        this IEnumerable<TSource> elements,
        Func<TSource, TResult> selector,
        EPositions position
    )
    {
        var coordinates = elements.Select(selector).ToList();
        coordinates.Sort();

        return position switch
        {
            EPositions.First => coordinates.First(),
            EPositions.Last  => coordinates.Last(),
            _               => throw new ArgumentOutOfRangeException(nameof(position), position, null)
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

    public static Point3D GetLowPoint3D(this Positioning positioning) =>
        new()
        {
            X = positioning.Coordinate.X - positioning.BoundsDistance.X,
            Y = positioning.Coordinate.Y - positioning.BoundsDistance.Y,
            Z = positioning.Coordinate.Z - positioning.BoundsDistance.Z
        };

    public static Point3D GetHighPoint3D(this Positioning positioning) =>
        new()
        {
            X = positioning.Coordinate.X + positioning.BoundsDistance.X,
            Y = positioning.Coordinate.Y + positioning.BoundsDistance.Y,
            Z = positioning.Coordinate.Z + positioning.BoundsDistance.Z
        };
}
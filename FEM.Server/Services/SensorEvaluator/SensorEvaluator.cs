using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Server.Data;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Services.BasisFunctionProvider;
using Vector = FEM.Server.Data.Domain.Vector;

namespace FEM.Server.Services.SensorEvaluator;

public class SensorEvaluator : ISensorEvaluator
{
    private readonly IBasisFunctionProvider _basisProvider;

    public SensorEvaluator(IBasisFunctionProvider basisProvider)
    {
        _basisProvider = basisProvider;
    }

    /// <inheritdoc />
    public double EvaluateBAtSensor(Sensor sensor, Mesh mesh, Vector solution)
    {
        // Найти конечный элемент, в котором находится сенсор
        var element = mesh.Elements.FirstOrDefault(e => e.Contains(sensor.Position));
        if (element is null)
            return 0.0;

        // Получить локальные значения потенциала A
        var aLocal = element.Edges.OrderBy(e => e.EdgeIndex).Select(e => solution[e.EdgeIndex]).ToArray();

        // Вычислить векторное магнитное поле B как сумму роторов базисных функций
        var b = new Vector3D();

        for (int i = 0; i < 12; i++)
        {
            var curl = _basisProvider.GetCurl(element, i, sensor.Position);
            b.X += aLocal[i] * curl.X;
            b.Y += aLocal[i] * curl.Y;
            b.Z += aLocal[i] * curl.Z;
        }

        return sensor.ComponentDirection switch
        {
            ESensorComponent.Bx => b.X,
            ESensorComponent.By => b.Y,
            ESensorComponent.Bz => b.Z,
            _                   => throw new ArgumentException()
        };
    }

    /// <inheritdoc />
    public Vector EvaluateAll(IReadOnlyList<Sensor> sensors, Mesh mesh, Vector solution)
    {
        var result = new Vector(sensors.Count);
        for (int i = 0; i < sensors.Count; i++)
        {
            result[i] = EvaluateBAtSensor(sensors[i], mesh, solution);
        }

        return result;
    }

    public Vector3D EvaluateFullBAtPoint(Point3D point, Mesh mesh, Vector solution)
    {
        var element = mesh.Elements.FirstOrDefault(e => e.Contains(point));
        if (element is null)
            return new(0, 0, 0);

        var aLocal = element.Edges.OrderBy(e => e.EdgeIndex).Select(e => solution[e.EdgeIndex]).ToArray();

        var b = new Vector3D();

        for (int i = 0; i < 12; i++)
        {
            var curl = _basisProvider.GetCurl(element, i, point);
            b.X += aLocal[i] * curl.X;
            b.Y += aLocal[i] * curl.Y;
            b.Z += aLocal[i] * curl.Z;
        }

        return b;
    }
}
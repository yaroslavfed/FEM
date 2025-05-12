using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using Vector = FEM.Server.Data.Domain.Vector;

namespace FEM.Server.Services.SensorEvaluator;

public interface ISensorEvaluator
{
    /// <summary>
    /// Вычисляет значение одной компоненты магнитного поля B в заданной точке (сенсоре).
    /// </summary>
    double EvaluateBAtSensor(Sensor sensor, Mesh mesh, Vector solution);

    /// <summary>
    /// Вычисляет значения поля B во всех сенсорах.
    /// </summary>
    Vector EvaluateAll(IReadOnlyList<Sensor> sensors, Mesh mesh, Vector solution);

    Vector3D EvaluateFullBAtPoint(Point3D point, Mesh mesh, Vector solution);
}
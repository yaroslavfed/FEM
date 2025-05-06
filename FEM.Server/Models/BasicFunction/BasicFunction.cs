using System.Numerics;
using AutoMapper;
using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Extensions;
using Vector = FEM.Common.Data.MathModels.Vector;

namespace FEM.Server.Models.BasicFunction;

public class BasicFunction : IBasicFunction
{
    private readonly IMapper _mapper;

    public BasicFunction(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Vector GetBasicFunctions(FiniteElement finiteElement, int? number, Sensor? position)
    {
        var feBounds = MapFiniteElementsAsync(finiteElement);

        return number switch
        {
            1 => new()
            {
                Data =
                [
                    HierarchicalFunctionsMinus(
                        feBounds.LowCoordinate.Y,
                        feBounds.HighCoordinate.Y,
                        position!.Coordinate.Y
                    )
                    * HierarchicalFunctionsMinus(
                        feBounds.LowCoordinate.Z,
                        feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),

                    0,

                    0
                ]
            },
            2 => new()
            {
                Data =
                [
                    HierarchicalFunctionsPlus(
                        feBounds.LowCoordinate.Y,
                        feBounds.HighCoordinate.Y,
                        position!.Coordinate.Y
                    )
                    * HierarchicalFunctionsMinus(
                        feBounds.LowCoordinate.Z,
                        feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),

                    0,

                    0
                ]
            },
            3 => new()
            {
                Data =
                [
                    HierarchicalFunctionsMinus(
                        feBounds.LowCoordinate.Y,
                        feBounds.HighCoordinate.Y,
                        position!.Coordinate.Y
                    )
                    * HierarchicalFunctionsPlus(
                        feBounds.LowCoordinate.Z,
                        feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),

                    0,

                    0
                ]
            },
            4 => new()
            {
                Data =
                [
                    HierarchicalFunctionsPlus(
                        feBounds.LowCoordinate.Y,
                        feBounds.HighCoordinate.Y,
                        position!.Coordinate.Y
                    )
                    * HierarchicalFunctionsPlus(
                        feBounds.LowCoordinate.Z,
                        feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),

                    0,

                    0
                ]
            },
            5 => new()
            {
                Data =
                [
                    0,

                    HierarchicalFunctionsMinus(
                        feBounds.LowCoordinate.X,
                        feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsMinus(
                        feBounds.LowCoordinate.Z,
                        feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),

                    0
                ]
            },
            6 => new()
            {
                Data =
                [
                    0,

                    HierarchicalFunctionsPlus(
                        feBounds.LowCoordinate.X,
                        feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsMinus(
                        feBounds.LowCoordinate.Z,
                        feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),

                    0
                ]
            },
            7 => new()
            {
                Data =
                [
                    0,

                    HierarchicalFunctionsMinus(
                        feBounds.LowCoordinate.X,
                        feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsPlus(
                        feBounds.LowCoordinate.Z,
                        feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),

                    0
                ]
            },
            8 => new()
            {
                Data =
                [
                    0,

                    HierarchicalFunctionsPlus(
                        feBounds.LowCoordinate.X,
                        feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsPlus(
                        feBounds.LowCoordinate.Z,
                        feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),

                    0
                ]
            },
            9 => new()
            {
                Data =
                [
                    0,
                    0,

                    HierarchicalFunctionsMinus(
                        feBounds.LowCoordinate.X,
                        feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsMinus(
                        feBounds.LowCoordinate.Y,
                        feBounds.HighCoordinate.Y,
                        position.Coordinate.Y
                    )
                ]
            },
            10 => new()
            {
                Data =
                [
                    0,
                    0,

                    HierarchicalFunctionsPlus(
                        feBounds.LowCoordinate.X,
                        feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsMinus(
                        feBounds.LowCoordinate.Y,
                        feBounds.HighCoordinate.Y,
                        position.Coordinate.Y
                    )
                ]
            },
            11 => new()
            {
                Data =
                [
                    0,
                    0,

                    HierarchicalFunctionsMinus(
                        feBounds.LowCoordinate.X,
                        feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsPlus(
                        feBounds.LowCoordinate.Y,
                        feBounds.HighCoordinate.Y,
                        position.Coordinate.Y
                    )
                ]
            },
            12 => new()
            {
                Data =
                [
                    0,
                    0,

                    HierarchicalFunctionsPlus(
                        feBounds.LowCoordinate.X,
                        feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsPlus(
                        feBounds.LowCoordinate.Y,
                        feBounds.HighCoordinate.Y,
                        position.Coordinate.Y
                    )
                ]
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public Vector3 GetCurl(FiniteElement element, int number, Sensor point)
    {
        const double h = 1e-6;

        var p = point.Coordinate;

        // Малые смещения по каждой оси
        var dx = new Point3D(p.X + h, p.Y, p.Z);
        var dy = new Point3D(p.X, p.Y + h, p.Z);
        var dz = new Point3D(p.X, p.Y, p.Z + h);

        var fx = GetBasicFunctions(element, number, new Sensor { Coordinate = p }).Data;
        var fxDx = GetBasicFunctions(element, number, new Sensor { Coordinate = dx }).Data;
        var fxDy = GetBasicFunctions(element, number, new Sensor { Coordinate = dy }).Data;
        var fxDz = GetBasicFunctions(element, number, new Sensor { Coordinate = dz }).Data;

        // Производные компонент по координатам (d/dx, d/dy, d/dz)
        var dAxDy = (fxDy[0] - fx[0]) / h;
        var dAxDz = (fxDz[0] - fx[0]) / h;

        var dAyDx = (fxDx[1] - fx[1]) / h;
        var dAyDz = (fxDz[1] - fx[1]) / h;

        var dAzDx = (fxDx[2] - fx[2]) / h;
        var dAzDy = (fxDy[2] - fx[2]) / h;

        // Вычисление ротора curl A = (∂Az/∂y - ∂Ay/∂z, ∂Ax/∂z - ∂Az/∂x, ∂Ay/∂x - ∂Ax/∂y)
        var curlX = dAzDy - dAyDz;
        var curlY = dAxDz - dAzDx;
        var curlZ = dAyDx - dAxDy;

        return new((float)curlX, (float)curlY, (float)curlZ);
    }

    private FiniteElementBounds MapFiniteElementsAsync(FiniteElement finiteElement)
    {
        return _mapper.Map<FiniteElementBounds>(finiteElement);
    }

    private static double HierarchicalFunctionsMinus(double startPoint, double endPoint, double position) =>
        (endPoint - position) / (endPoint - startPoint);

    private static double HierarchicalFunctionsPlus(double startPoint, double endPoint, double position) =>
        (position - startPoint) / (endPoint - startPoint);
}
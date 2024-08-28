using AutoMapper;
using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using VectorFEM.Core.Data.Parallelepipedal;

namespace VectorFEM.Core.Models.Parallelepipedal.BasicFunction;

public class BasicFunction : IBasicFunction<Vector>
{
    private readonly FiniteElementBounds _feBounds;

    public BasicFunction(
        FiniteElement finiteElement,
        IMapper mapper
    )
    {
        _feBounds = mapper.Map<FiniteElementBounds>(finiteElement);
    }

    public Vector GetBasicFunctions(int? number, Sensor? position) =>
        number switch
        {
            1 => new()
            {
                Data = new List<double>
                {
                    HierarchicalFunctionsMinus(
                        _feBounds.LowCoordinate.Y,
                        _feBounds.HighCoordinate.Y,
                        position!.Coordinate.Y
                    )
                    * HierarchicalFunctionsMinus(
                        _feBounds.LowCoordinate.Z,
                        _feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),
                    0,
                    0
                }
            },
            2 => new()
            {
                Data = new List<double>
                {
                    HierarchicalFunctionsPlus(
                        _feBounds.LowCoordinate.Y,
                        _feBounds.HighCoordinate.Y,
                        position!.Coordinate.Y
                    )
                    * HierarchicalFunctionsMinus(
                        _feBounds.LowCoordinate.Z,
                        _feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),
                    0,
                    0
                }
            },
            3 => new()
            {
                Data = new List<double>
                {
                    HierarchicalFunctionsMinus(
                        _feBounds.LowCoordinate.Y,
                        _feBounds.HighCoordinate.Y,
                        position!.Coordinate.Y
                    )
                    * HierarchicalFunctionsPlus(
                        _feBounds.LowCoordinate.Z,
                        _feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),
                    0,
                    0
                }
            },
            4 => new()
            {
                Data = new List<double>
                {
                    HierarchicalFunctionsPlus(
                        _feBounds.LowCoordinate.Y,
                        _feBounds.HighCoordinate.Y,
                        position!.Coordinate.Y
                    )
                    * HierarchicalFunctionsPlus(
                        _feBounds.LowCoordinate.Z,
                        _feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),
                    0,
                    0
                }
            },
            5 => new()
            {
                Data = new List<double>
                {
                    0,
                    HierarchicalFunctionsMinus(
                        _feBounds.LowCoordinate.X,
                        _feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsMinus(
                        _feBounds.LowCoordinate.Z,
                        _feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),
                    0
                }
            },
            6 => new()
            {
                Data = new List<double>
                {
                    0,
                    HierarchicalFunctionsPlus(
                        _feBounds.LowCoordinate.X,
                        _feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsMinus(
                        _feBounds.LowCoordinate.Z,
                        _feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),
                    0
                }
            },
            7 => new()
            {
                Data = new List<double>
                {
                    0,
                    HierarchicalFunctionsMinus(
                        _feBounds.LowCoordinate.X,
                        _feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsPlus(
                        _feBounds.LowCoordinate.Z,
                        _feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),
                    0
                }
            },
            8 => new()
            {
                Data = new List<double>
                {
                    0,
                    HierarchicalFunctionsPlus(
                        _feBounds.LowCoordinate.X,
                        _feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsPlus(
                        _feBounds.LowCoordinate.Z,
                        _feBounds.HighCoordinate.Z,
                        position.Coordinate.Z
                    ),
                    0
                }
            },
            9 => new()
            {
                Data = new List<double>
                {
                    0,
                    0,
                    HierarchicalFunctionsMinus(
                        _feBounds.LowCoordinate.X,
                        _feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsMinus(
                        _feBounds.LowCoordinate.Y,
                        _feBounds.HighCoordinate.Y,
                        position.Coordinate.Y
                    )
                }
            },
            10 => new()
            {
                Data = new List<double>
                {
                    0,
                    0,
                    HierarchicalFunctionsPlus(
                        _feBounds.LowCoordinate.X,
                        _feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsMinus(
                        _feBounds.LowCoordinate.Y,
                        _feBounds.HighCoordinate.Y,
                        position.Coordinate.Y
                    )
                }
            },
            11 => new()
            {
                Data = new List<double>
                {
                    0,
                    0,
                    HierarchicalFunctionsMinus(
                        _feBounds.LowCoordinate.X,
                        _feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsPlus(
                        _feBounds.LowCoordinate.Y,
                        _feBounds.HighCoordinate.Y,
                        position.Coordinate.Y
                    )
                }
            },
            12 => new()
            {
                Data = new List<double>
                {
                    0,
                    0,
                    HierarchicalFunctionsPlus(
                        _feBounds.LowCoordinate.X,
                        _feBounds.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsPlus(
                        _feBounds.LowCoordinate.Y,
                        _feBounds.HighCoordinate.Y,
                        position.Coordinate.Y
                    )
                }
            },
            _ => throw new ArgumentOutOfRangeException()
        };

    private static double HierarchicalFunctionsMinus(double startPoint, double endPoint, double position) =>
        (endPoint - position) / (endPoint - startPoint);

    private static double HierarchicalFunctionsPlus(double startPoint, double endPoint, double position) =>
        (position - startPoint) / (endPoint - startPoint);
}
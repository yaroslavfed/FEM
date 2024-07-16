using AutoMapper;
using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Core.Data.Dto;

namespace FEM.Core.Models.VectorFEM;

internal class BasicVectorFunction : IBasicFunction<Vector>
{
    private readonly FiniteElementDto _feDto;

    public BasicVectorFunction(
        FiniteElement finiteElement,
        IMapper mapper
    )
    {
        _feDto = mapper.Map<FiniteElementDto>(finiteElement);
    }

    public Vector GetBasicFunctions(int? number, Sensor? position) =>
        number switch
        {
            1 => new()
            {
                Data = new List<double>
                {
                    HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
                        position!.Coordinate.Y
                    )
                    * HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
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
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
                        position!.Coordinate.Y
                    )
                    * HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
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
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
                        position!.Coordinate.Y
                    )
                    * HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
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
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
                        position!.Coordinate.Y
                    )
                    * HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
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
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
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
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
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
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
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
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
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
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
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
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
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
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
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
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X
                    )
                    * HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
                        position.Coordinate.Y
                    )
                }
            },
            _ => throw new ArgumentOutOfRangeException()
        };

    private double HierarchicalFunctionsMinus(double startPoint, double endPoint, double position) =>
        (endPoint - position) / (endPoint - startPoint);

    private double HierarchicalFunctionsPlus(double startPoint, double endPoint, double position) =>
        (position - startPoint) / (endPoint - startPoint);
}
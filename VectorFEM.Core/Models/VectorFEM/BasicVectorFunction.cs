using AutoMapper;
using VectorFEM.Shared.Domain;
using VectorFEM.Shared.Domain.Dto;
using VectorFEM.Shared.Domain.MathModels;

namespace VectorFEM.Core.Models.VectorFEM;

internal class BasicVectorFunction : IBasicFunction<Vector>
{
    private readonly FiniteElementDto _feDto;

    public BasicVectorFunction(
        FiniteElement finiteElement,
        IMapper mapper)
    {
        _feDto = mapper.Map<FiniteElementDto>(finiteElement);
    }

    public Vector GetBasicFunctions(int? number, Sensor? position)
    {
        return number switch
        {
            1 => new Vector
            {
                Data = new List<double>
                {
                    HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
                        position!.Coordinate.Y)
                    * HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
                        position.Coordinate.Z),
                    0,
                    0
                }
            },
            2 => new Vector
            {
                Data = new List<double>
                {
                    HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
                        position!.Coordinate.Y)
                    * HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
                        position.Coordinate.Z),
                    0,
                    0
                }
            },
            3 => new Vector
            {
                Data = new List<double>
                {
                    HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
                        position!.Coordinate.Y)
                    * HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
                        position.Coordinate.Z),
                    0,
                    0
                }
            },
            4 => new Vector
            {
                Data = new List<double>
                {
                    HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
                        position!.Coordinate.Y)
                    * HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
                        position.Coordinate.Z),
                    0,
                    0
                }
            },
            5 => new Vector
            {
                Data = new List<double>
                {
                    0,
                    HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X)
                    * HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
                        position.Coordinate.Z),
                    0
                }
            },
            6 => new Vector
            {
                Data = new List<double>
                {
                    0,
                    HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X)
                    * HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
                        position.Coordinate.Z),
                    0
                }
            },
            7 => new Vector
            {
                Data = new List<double>
                {
                    0,
                    HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X)
                    * HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
                        position.Coordinate.Z),
                    0
                }
            },
            8 => new Vector
            {
                Data = new List<double>
                {
                    0,
                    HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X)
                    * HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.Z,
                        _feDto.HighCoordinate.Z,
                        position.Coordinate.Z),
                    0
                }
            },
            9 => new Vector
            {
                Data = new List<double>
                {
                    0,
                    0,
                    HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X)
                    * HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
                        position.Coordinate.Y)
                }
            },
            10 => new Vector
            {
                Data = new List<double>
                {
                    0,
                    0,
                    HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X)
                    * HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
                        position.Coordinate.Y)
                }
            },
            11 => new Vector
            {
                Data = new List<double>
                {
                    0,
                    0,
                    HierarchicalFunctionsMinus(
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X)
                    * HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
                        position.Coordinate.Y)
                }
            },
            12 => new Vector
            {
                Data = new List<double>
                {
                    0,
                    0,
                    HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.X,
                        _feDto.HighCoordinate.X,
                        position!.Coordinate.X)
                    * HierarchicalFunctionsPlus(
                        _feDto.LowCoordinate.Y,
                        _feDto.HighCoordinate.Y,
                        position.Coordinate.Y)
                }
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double HierarchicalFunctionsMinus(double startPoint, double endPoint, double position) =>
        (endPoint - position) / (endPoint - startPoint);

    private double HierarchicalFunctionsPlus(double startPoint, double endPoint, double position) =>
        (position - startPoint) / (endPoint - startPoint);
}
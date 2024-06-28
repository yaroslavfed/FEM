using AutoMapper;
using FEM.Shared.Domain.Data;
using FEM.Shared.Domain.Dto;
using FEM.Shared.Domain.MathModels;

namespace FEM.Core.Models.VectorFEM;

internal class MassVectorMatrix : IMassMatrix<Matrix>
{
    private readonly FiniteElementDto _feDto;

    private readonly IReadOnlyList<IReadOnlyList<double>> _massMatrix =
    [
        [4, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0],
        [2, 4, 1, 2, 0, 0, 0, 0, 0, 0, 0, 0],
        [2, 1, 4, 2, 0, 0, 0, 0, 0, 0, 0, 0],
        [1, 2, 2, 4, 0, 0, 0, 0, 0, 0, 0, 0],
        [0, 0, 0, 0, 4, 2, 2, 1, 0, 0, 0, 0],
        [0, 0, 0, 0, 2, 4, 1, 2, 0, 0, 0, 0],
        [0, 0, 0, 0, 2, 1, 4, 2, 0, 0, 0, 0],
        [0, 0, 0, 0, 1, 2, 2, 4, 0, 0, 0, 0],
        [0, 0, 0, 0, 0, 0, 0, 0, 4, 2, 2, 1],
        [0, 0, 0, 0, 0, 0, 0, 0, 2, 4, 1, 2],
        [0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 4, 2],
        [0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 4],
    ];

    public MassVectorMatrix(
        FiniteElement finiteElement,
        IMapper mapper
    )
    {
        _feDto = mapper.Map<FiniteElementDto>(finiteElement);
    }

    public Matrix GetMassMatrix(double gamma)
    {
        var matrix = new Matrix
        {
            Data = _massMatrix
        };
        matrix *= gamma
                  * (_feDto.HighCoordinate.X - _feDto.LowCoordinate.X)
                  * (_feDto.HighCoordinate.Y - _feDto.LowCoordinate.Y)
                  * (_feDto.HighCoordinate.Z - _feDto.LowCoordinate.Z)
                  / 36;

        return matrix;
    }
}
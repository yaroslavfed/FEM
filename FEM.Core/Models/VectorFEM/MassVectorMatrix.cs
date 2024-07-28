using AutoMapper;
using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Core.Data.Dto;

namespace FEM.Core.Models.VectorFEM;

internal class MassVectorMatrix : IMassMatrix<Matrix>
{
    private readonly FiniteElementBounds _feBounds;

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
        [0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 4]
    ];

    public MassVectorMatrix(
        FiniteElement finiteElement,
        IMapper mapper
    )
    {
        _feBounds = mapper.Map<FiniteElementBounds>(finiteElement);
    }

    public Matrix GetMassMatrix(double gamma)
    {
        var matrix = new Matrix { Data = _massMatrix };
        matrix *= gamma
                  * (_feBounds.HighCoordinate.X - _feBounds.LowCoordinate.X)
                  * (_feBounds.HighCoordinate.Y - _feBounds.LowCoordinate.Y)
                  * (_feBounds.HighCoordinate.Z - _feBounds.LowCoordinate.Z)
                  / 36;

        return matrix;
    }
}
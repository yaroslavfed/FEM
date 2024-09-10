using AutoMapper;
using FEM.Common.Data.MathModels;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Models.Parallelepipedal.MassMatrix;

public class MassMatrix : IMassMatrix<Matrix>
{
    private readonly IMapper _mapper;

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

    public MassMatrix(IMapper mapper)
    {
        _mapper = mapper;
    }

    public IReadOnlyList<IReadOnlyList<double>> MassMatrixBase => _massMatrix;

    public Task<Matrix> GetMassMatrixAsync(double gamma, FiniteElement finiteElement)
    {
        var feBounds = _mapper.Map<FiniteElementBounds>(finiteElement);

        var matrix = new Matrix { Data = _massMatrix };
        matrix *= gamma
                  * (feBounds.HighCoordinate.X - feBounds.LowCoordinate.X)
                  * (feBounds.HighCoordinate.Y - feBounds.LowCoordinate.Y)
                  * (feBounds.HighCoordinate.Z - feBounds.LowCoordinate.Z)
                  / 36;

        return Task.FromResult(matrix);
    }
}
using System.Numerics;
using AutoMapper;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Models.Parallelepipedal.BasicFunction;

namespace FEM.Server.Models.CurrentSource;

public class LineCurrentSource : ICurrentSource
{
    private readonly Vector3      _start;
    private readonly Vector3      _end;
    private readonly double       _current;
    private readonly int          _segments;
    private readonly IMapper      _mapper;
    private readonly SpatialIndex _spatialIndex;

    public LineCurrentSource(
        Vector3 start,
        Vector3 end,
        double current,
        int segments,
        IMapper mapper,
        SpatialIndex spatialIndex
    )
    {
        _start = start;
        _end = end;
        _current = current;
        _segments = segments;
        _mapper = mapper;
        _spatialIndex = spatialIndex;
    }

    public async Task<double[]> GetElementSourceAsync(FiniteElement element)
    {
        var localRightPart = new double[12];
        var dl = (_end - _start) / _segments;

        for (int i = 0; i < _segments; i++)
        {
            var p1 = _start + dl * i;
            var p2 = _start + dl * (i + 1);
            var mid = 0.5f * (p1 + p2);

            var el = _spatialIndex.FindClosestElement(mid);
            if (el == null || el != element)
                continue;

            var basis = new BasicFunction(el, _mapper);
            for (int j = 0; j < 12; j++)
            {
                var phi = basis.GetBasicFunctions(
                    j + 1,
                    new() { Coordinate = new() { X = mid.X, Y = mid.Y, Z = mid.Z } }
                );
                var projection = phi.Data[0] * dl.X + phi.Data[1] * dl.Y + phi.Data[2] * dl.Z;
                localRightPart[j] += _current * projection * dl.Length() / 2.0;
            }
        }

        return localRightPart;
    }
}
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Extensions;
using FEM.Server.Services.BasisFunctionProvider;

namespace FEM.Server.Services.VectorBuilder;

public class VectorBuilder : IVectorBuilder
{
    private readonly IBasisFunctionProvider _basisFunctionProvider;

    public VectorBuilder(IBasisFunctionProvider basisFunctionProvider)
    {
        _basisFunctionProvider = basisFunctionProvider;
    }

    public Task<Vector> ComputeLocalRightHandSideAsync(FiniteElement element, IEnumerable<CurrentSegment> sources)
    {
        const int edgeCount = 12;
        var localVector = new Vector(edgeCount);

        var volume = element.GetSizes().X * element.GetSizes().Y * element.GetSizes().Z;

        foreach (var source in sources)
        {
            if (!element.Contains(source.Center)) continue;

            for (int i = 0; i < edgeCount; i++)
            {
                var basis = _basisFunctionProvider.GetValue(element, i, source.Center);
                double contribution = basis.Dot(source.Direction) * source.Current;

                localVector[i] += contribution * volume;
            }
        }

        return Task.FromResult(localVector);
    }
}
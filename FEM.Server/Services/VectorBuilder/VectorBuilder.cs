using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Extensions;
using FEM.Server.Models.CurrentSource;
using Vector = FEM.Server.Data.Domain.Vector;

namespace FEM.Server.Services.VectorBuilder;

public class VectorBuilder : IVectorBuilder
{
    public async Task<Vector> ComputeLocalRightHandSideAsync(FiniteElement element, IEnumerable<ICurrentSource> sources)
    {
        const int edgeCount = 12;
        var vector = new double[edgeCount];

        // Центр элемента для простой оценки J
        var center = new Sensor(0.0, 0.0, 0.0);

        // Получим J в центре элемента (можно расширить на интегрирование по всей области)
        var J = Vector.Zero;
        foreach (var source in sources)
        {
            J += await source.GetFieldAsync(center, element);
        }

        for (int i = 0; i < edgeCount; i++)
        {
            var basisFunction = await _basisFunctionService.GetBasisFunctionAsync(element, i);
            var Wi = basisFunction(center);

            var dot = J.X * Wi.X + J.Y * Wi.Y + J.Z * Wi.Z;

            // Масштаб объема
            var volume = element.GetSizes().X * element.GetSizes().Y * element.GetSizes().Z;
            vector[i] = dot * volume;
        }

        return new Vector(vector);
    }

}
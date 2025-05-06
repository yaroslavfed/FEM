using FEM.Common.Data.MathModels;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Models.CurrentSource;
using Vector = FEM.Server.Data.Domain.Vector;

namespace FEM.Server.Services.VectorBuilder;

public interface IVectorBuilder
{
    Task<Vector> ComputeLocalRightHandSideAsync(FiniteElement element, IEnumerable<ICurrentSource> sources);
}
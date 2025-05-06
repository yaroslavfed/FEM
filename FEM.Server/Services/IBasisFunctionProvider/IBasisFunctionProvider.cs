using System.Numerics;
using FEM.Common.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using Vector = FEM.Common.Data.MathModels.Vector;

namespace FEM.Server.Services.IBasisFunctionProvider;

public interface IBasisFunctionProvider
{
    Vector GetValue(FiniteElement element, int edgeNumber, Sensor point);

    Vector3 GetCurl(FiniteElement element, int edgeNumber, Sensor point);
}
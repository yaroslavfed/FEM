using System.Numerics;
using FEM.Common.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Models.BasicFunction;
using FEM.Server.Services.IBasisFunctionProvider;
using Vector = FEM.Common.Data.MathModels.Vector;

public class BasicFunctionProvider : IBasisFunctionProvider
{
    private readonly IBasicFunction _basicFunction;

    public BasicFunctionProvider(IBasicFunction basicFunction)
    {
        _basicFunction = basicFunction;
    }

    public Vector GetValue(FiniteElement element, int edgeNumber, Sensor point)
    {
        return _basicFunction.GetBasicFunctions(element, edgeNumber, point);
    }

    public Vector3 GetCurl(FiniteElement element, int edgeNumber, Sensor point)
    {
        return _basicFunction.GetCurl(element, edgeNumber, point);
    }
}
using FEM.Common.Data.MathModels;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Models.BasicFunction;

namespace FEM.Server.Services.BasisFunctionProvider;

public class BasicFunctionProvider : IBasisFunctionProvider
{
    private readonly IBasicFunction _basicFunction;

    public BasicFunctionProvider(IBasicFunction basicFunction)
    {
        _basicFunction = basicFunction;
    }

    public Vector3D GetValue(FiniteElement element, int edgeNumber, Point3D point)
    {
        return _basicFunction.GetBasicFunctions(element, edgeNumber, point);
    }

    public Vector3D GetCurl(FiniteElement element, int edgeNumber, Point3D point)
    {
        return _basicFunction.GetCurl(element, edgeNumber, point);
    }
}
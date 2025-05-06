using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Models.BasicFunction;
using FEM.Server.Services.IBasisFunctionProvider;

public class BasicFunctionProvider : IBasisFunctionProvider
{
    private readonly IBasicFunction _basicFunction;

    public BasicFunctionProvider(IBasicFunction basicFunction)
    {
        _basicFunction = basicFunction;
    }

    public Vector3D GetValue(FiniteElement element, int edgeNumber, Point3D point)
    {
        return _basicFunction.GetValue(element, edgeNumber, point);
    }

    public Vector3D GetCurl(FiniteElement element, int edgeNumber, Point3D point)
    {
        return _basicFunction.GetCurl(element, edgeNumber, point);
    }
}
using FEM.Common.Data.MathModels;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Services.BasisFunctionProvider;

namespace FEM.UnitTests;

public class BasicFunctionProviderStub : IBasisFunctionProvider
{
    public Vector3D GetCurl(FiniteElement element, int number, Point3D point)
    {
        return number switch
        {
            0 => new(1, 0, 0),
            1 => new(0, 1, 0),
            2 => new(0, 0, 1),
            _ => new(0, 0, 0)
        };
    }

    public Vector3D GetValue(FiniteElement element, int number, Point3D point)
    {
        return new(0, 0, 0);
    }
}
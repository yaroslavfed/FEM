using FEM.Common.Data.MathModels;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.IBasisFunctionProvider;

public interface IBasisFunctionProvider
{
    Vector3D GetValue(FiniteElement element, int edgeNumber, Point3D point);

    Vector3D GetCurl(FiniteElement element, int edgeNumber, Point3D point);
}
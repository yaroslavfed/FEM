using FEM.Common.Data.MathModels;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Models.BasicFunction;

public interface IBasicFunction
{
    Vector3D GetBasicFunctions(FiniteElement finiteElement, int? number, Point3D? position);

    Vector3D GetCurl(FiniteElement element, int number, Point3D point);
}
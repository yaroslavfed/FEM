using System.Numerics;
using FEM.Common.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using Vector = FEM.Common.Data.MathModels.Vector;

namespace FEM.Server.Models.BasicFunction;

public interface IBasicFunction
{
    Vector GetBasicFunctions(FiniteElement finiteElement, int? number, Sensor? position);

    Vector3 GetCurl(FiniteElement element, int number, Sensor point);
}
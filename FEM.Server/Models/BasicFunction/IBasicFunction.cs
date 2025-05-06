using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Models.BasicFunction;

public interface IBasicFunction
{
    Vector GetBasicFunctions(FiniteElement finiteElement, int? number = null, Sensor? sensor = null);
}
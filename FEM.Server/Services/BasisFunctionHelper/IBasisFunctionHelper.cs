using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Models.BasicFunction;
using Vector = FEM.Server.Data.Domain.Vector;

namespace FEM.Server.Services.BasisFunctionHelper;

public interface IBasisFunctionHelper
{
    public static Vector GetPhysicalCurl(
        FiniteElement element,
        int basisNumber,
        Sensor point,
        BasicFunction basicFunction
    )
}
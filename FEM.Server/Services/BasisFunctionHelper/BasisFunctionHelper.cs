using FEM.Common.Data.Domain;
using FEM.Common.Data.MathModels;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Models.BasicFunction;
using Vector = FEM.Server.Data.Domain.Vector;

namespace FEM.Server.Services.BasisFunctionHelper;

public class BasisFunctionHelper : IBasisFunctionHelper
{
    // Возвращает ротор базисной функции в физической системе координат
    public static Vector GetPhysicalCurl(
        FiniteElement element,
        int basisNumber,
        Sensor point,
        BasicFunction basicFunction
    )
    {
        // Получаем ротор в эталонной системе координат
        var curlRef = basicFunction.GetCurl(element, basisNumber, point);

        // Получаем размеры параллелепипеда
        var hx = element.Sizes.X;
        var hy = element.Sizes.Y;
        var hz = element.Sizes.Z;

        // Строим матрицу Якоби для аффинного отображения эталонного куба [-1, 1]^3 на текущий элемент
        var jacobian = new double[,]
        {
            {
                hx / 2.0,
                0,
                0
            },
            {
                0,
                hy / 2.0,
                0
            },
            {
                0,
                0,
                hz / 2.0
            }
        };

        // Строим обратную транспонированную матрицу Якоби
        var jacobianInvT = new double[3, 3];
        jacobianInvT[0, 0] = 2.0 / hx;
        jacobianInvT[1, 1] = 2.0 / hy;
        jacobianInvT[2, 2] = 2.0 / hz;

        // Умножаем J^(-T) * rot(Ŵ)
        var physicalCurl = new Vector(
            jacobianInvT[0, 0] * curlRef.X + jacobianInvT[0, 1] * curlRef.Y + jacobianInvT[0, 2] * curlRef.Z,
            jacobianInvT[1, 0] * curlRef.X + jacobianInvT[1, 1] * curlRef.Y + jacobianInvT[1, 2] * curlRef.Z,
            jacobianInvT[2, 0] * curlRef.X + jacobianInvT[2, 1] * curlRef.Y + jacobianInvT[2, 2] * curlRef.Z
        );

        return physicalCurl;
    }
}
using VectorFEM.Data;

namespace VectorFEM.Models.VectorFEM;

public class BasicVectorFunction : IBasicFunction<Vector>
{
    public Vector GetBasicFunctions(FiniteElement element, int? number, Sensor? position)
    {
        return number switch
        {
            1 => new Vector
            {
                Data = new List<double>()
                {
                    HierarchicalFunctionsMinus(element.Y0, element.Yn, position!.Y)
                    * HierarchicalFunctionsMinus(element.Z0, element.Zn, position.Z),
                    0,
                    0
                }
            },
            2 => new Vector
            {
                Data = new List<double>()
                {
                    HierarchicalFunctionsPlus(element.Y0, element.Yn, position!.Y)
                    * HierarchicalFunctionsMinus(element.Z0, element.Zn, position.Z),
                    0,
                    0
                }
            },
            3 => new Vector
            {
                Data = new List<double>()
                {
                    HierarchicalFunctionsMinus(element.Y0, element.Yn, position!.Y)
                    * HierarchicalFunctionsPlus(element.Z0, element.Zn, position.Z),
                    0,
                    0
                }
            },
            4 => new Vector
            {
                Data = new List<double>()
                {
                    HierarchicalFunctionsPlus(element.Y0, element.Yn, position!.Y)
                    * HierarchicalFunctionsPlus(element.Z0, element.Zn, position.Z),
                    0,
                    0
                }
            },
            5 => new Vector
            {
                Data = new List<double>()
                {
                    0,
                    HierarchicalFunctionsMinus(element.X0, element.Xn, position!.X)
                    * HierarchicalFunctionsMinus(element.Z0, element.Zn, position.Z),
                    0
                }
            },
            6 => new Vector
            {
                Data = new List<double>()
                {
                    0,
                    HierarchicalFunctionsPlus(element.X0, element.Xn, position!.X)
                    * HierarchicalFunctionsMinus(element.Z0, element.Zn, position.Z),
                    0
                }
            },
            7 => new Vector
            {
                Data = new List<double>()
                {
                    0,
                    HierarchicalFunctionsMinus(element.X0, element.Xn, position!.X)
                    * HierarchicalFunctionsPlus(element.Z0, element.Zn, position.Z),
                    0
                }
            },
            8 => new Vector
            {
                Data = new List<double>()
                {
                    0,
                    HierarchicalFunctionsPlus(element.X0, element.Xn, position!.X)
                    * HierarchicalFunctionsPlus(element.Z0, element.Zn, position.Z),
                    0
                }
            },
            9 => new Vector
            {
                Data = new List<double>()
                {
                    0,
                    0,
                    HierarchicalFunctionsMinus(element.X0, element.Xn, position!.X)
                    * HierarchicalFunctionsMinus(element.Y0, element.Yn, position.Y)
                }
            },
            10 => new Vector
            {
                Data = new List<double>()
                {
                    0,
                    0,
                    HierarchicalFunctionsPlus(element.X0, element.Xn, position!.X)
                    * HierarchicalFunctionsMinus(element.Y0, element.Yn, position.Y)
                }
            },
            11 => new Vector
            {
                Data = new List<double>()
                {
                    0,
                    0,
                    HierarchicalFunctionsMinus(element.X0, element.Xn, position!.X)
                    * HierarchicalFunctionsPlus(element.Y0, element.Yn, position.Y)
                }
            },
            12 => new Vector
            {
                Data = new List<double>()
                {
                    0,
                    0,
                    HierarchicalFunctionsPlus(element.X0, element.Xn, position!.X)
                    * HierarchicalFunctionsPlus(element.Y0, element.Yn, position.Y)
                }
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private double HierarchicalFunctionsMinus(double startPoint, double endPoint, double position) =>
        (endPoint - position) / (endPoint - startPoint);

    private double HierarchicalFunctionsPlus(double startPoint, double endPoint, double position) =>
        (position - startPoint) / (endPoint - startPoint);
}
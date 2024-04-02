using VectorFEM.Models;

namespace VectorFEM;

public class Startup
{
    public async Task Run()
    {
        var position = new Sensor(0.5, 0.5, 0.5);

        var element = new FiniteElement(
            X0: -1, Xn: 1, Y0: -1, Yn: 1, Z0: -1, Zn: 1,
            Edges: Enumerable.Range(1, 12).Select(i => new Edge(i)).ToList());

        Console.WriteLine(GetMassMatrix(element, position).ToString());
    }

    private IReadOnlyList<Vector> GetBasicVectorFunction(FiniteElement element, Sensor position)
    {
        return element.Edges.Select(edge => edge.Number switch
        {
            1 => new Vector
            {
                Data = new List<double>()
                {
                    HierarchicalFunctionsMinus(element.Y0, element.Yn, position.Y)
                    * HierarchicalFunctionsMinus(element.Z0, element.Zn, position.Z),
                    0,
                    0
                }
            },
            2 => new Vector
            {
                Data = new List<double>()
                {
                    HierarchicalFunctionsPlus(element.Y0, element.Yn, position.Y)
                    * HierarchicalFunctionsMinus(element.Z0, element.Zn, position.Z),
                    0,
                    0
                }
            },
            3 => new Vector
            {
                Data = new List<double>()
                {
                    HierarchicalFunctionsMinus(element.Y0, element.Yn, position.Y)
                    * HierarchicalFunctionsPlus(element.Z0, element.Zn, position.Z),
                    0,
                    0
                }
            },
            4 => new Vector
            {
                Data = new List<double>()
                {
                    HierarchicalFunctionsPlus(element.Y0, element.Yn, position.Y)
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
                    HierarchicalFunctionsMinus(element.X0, element.Xn, position.X)
                    * HierarchicalFunctionsMinus(element.Z0, element.Zn, position.Z),
                    0
                }
            },
            6 => new Vector
            {
                Data = new List<double>()
                {
                    0,
                    HierarchicalFunctionsPlus(element.X0, element.Xn, position.X)
                    * HierarchicalFunctionsMinus(element.Z0, element.Zn, position.Z),
                    0
                }
            },
            7 => new Vector
            {
                Data = new List<double>()
                {
                    0,
                    HierarchicalFunctionsMinus(element.X0, element.Xn, position.X)
                    * HierarchicalFunctionsPlus(element.Z0, element.Zn, position.Z),
                    0
                }
            },
            8 => new Vector
            {
                Data = new List<double>()
                {
                    0,
                    HierarchicalFunctionsPlus(element.X0, element.Xn, position.X)
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
                    HierarchicalFunctionsMinus(element.X0, element.Xn, position.X)
                    * HierarchicalFunctionsMinus(element.Y0, element.Yn, position.Y)
                }
            },
            10 => new Vector
            {
                Data = new List<double>()
                {
                    0,
                    0,
                    HierarchicalFunctionsPlus(element.X0, element.Xn, position.X)
                    * HierarchicalFunctionsMinus(element.Y0, element.Yn, position.Y)
                }
            },
            11 => new Vector
            {
                Data = new List<double>()
                {
                    0,
                    0,
                    HierarchicalFunctionsMinus(element.X0, element.Xn, position.X)
                    * HierarchicalFunctionsPlus(element.Y0, element.Yn, position.Y)
                }
            },
            12 => new Vector
            {
                Data = new List<double>()
                {
                    0,
                    0,
                    HierarchicalFunctionsPlus(element.X0, element.Xn, position.X)
                    * HierarchicalFunctionsPlus(element.Y0, element.Yn, position.Y)
                }
            },
            _ => throw new ArgumentOutOfRangeException()
        }).ToList();
    }

    private double HierarchicalFunctionsMinus(double startPoint, double endPoint, double position) =>
        (endPoint - position) / (endPoint - startPoint);

    private double HierarchicalFunctionsPlus(double startPoint, double endPoint, double position) =>
        (position - startPoint) / (endPoint - startPoint);

    private Matrix GetMassMatrix(FiniteElement element, Sensor position)
    {
        var basicVectorFunctions = GetBasicVectorFunction(element, position);
        var matrix = new List<List<double>>();
        for (int i = 0; i < element.Edges.Count; i++)
        {
            var line = new List<double>();
            for (int j = 0; j < element.Edges.Count; j++)
            {
                var test = basicVectorFunctions[i]
                           * basicVectorFunctions[j];
                line.Add(
                    test
                );
            }

            matrix.Add(line);
        }

        return new Matrix()
        {
            Data = matrix
        };
    }

    //private Matrix GetStiffnessMatrix()
    //{
    //}
}
using FEM.Common.Data.MathModels;
using FEM.Server.Data.Domain;
using FEM.Server.Data.Parallelepipedal;
using FEM.Server.Services.BasisFunctionProvider;

namespace FEM.UnitTests;

public class BasicFunctionProviderStub : IBasisFunctionProvider
{
    public Vector3D GetCurl(FiniteElement element, int number, Point3D point)
    {
        var nodes = element.Edges.SelectMany(e => e.Nodes).DistinctBy(n => n.NodeIndex).ToList();

        var x0 = nodes.Min(n => n.Coordinate.X);
        var x1 = nodes.Max(n => n.Coordinate.X);
        var y0 = nodes.Min(n => n.Coordinate.Y);
        var y1 = nodes.Max(n => n.Coordinate.Y);
        var z0 = nodes.Min(n => n.Coordinate.Z);
        var z1 = nodes.Max(n => n.Coordinate.Z);

        double dx = x1 - x0;
        double dy = y1 - y0;
        double dz = z1 - z0;

        return number switch
        {
            0 => new Vector3D(0, -1.0 / dx / dz, 1.0 / dx / dy),  // edge (0,1)
            1 => new Vector3D(0, 1.0 / dx / dz, 1.0 / dx / dy),   // edge (2,3)
            2 => new Vector3D(0, -1.0 / dx / dz, -1.0 / dx / dy), // edge (4,5)
            3 => new Vector3D(0, 1.0 / dx / dz, -1.0 / dx / dy),  // edge (6,7)

            4 => new Vector3D(1.0 / dy / dz, 0, -1.0 / dx / dy),  // edge (0,2)
            5 => new Vector3D(-1.0 / dy / dz, 0, -1.0 / dx / dy), // edge (1,3)
            6 => new Vector3D(1.0 / dy / dz, 0, 1.0 / dx / dy),   // edge (4,6)
            7 => new Vector3D(-1.0 / dy / dz, 0, 1.0 / dx / dy),  // edge (5,7)

            8  => new Vector3D(-1.0 / dy / dz, 1.0 / dx / dz, 0),  // edge (0,4)
            9  => new Vector3D(1.0 / dy / dz, 1.0 / dx / dz, 0),   // edge (1,5)
            10 => new Vector3D(-1.0 / dy / dz, -1.0 / dx / dz, 0), // edge (2,6)
            11 => new Vector3D(1.0 / dy / dz, -1.0 / dx / dz, 0),  // edge (3,7)

            _ => throw new ArgumentOutOfRangeException(nameof(number), "Edge number must be in [0, 11].")
        };
    }

    public Vector3D GetValue(FiniteElement element, int edgeNumber, Point3D point)
    {
        return GetBasicFunctions(element, edgeNumber, point);
    }

    public Vector3D GetBasicFunctions(FiniteElement finiteElement, int? number, Point3D? position)
    {
        return (number + 1) switch
        {
            1 => new()
            {
                X = HierarchicalFunctionsMinus(
                        finiteElement.GetBounds().MinY,
                        finiteElement.GetBounds().MaxY,
                        position!.Y
                    )
                    * HierarchicalFunctionsMinus(
                        finiteElement.GetBounds().MinZ,
                        finiteElement.GetBounds().MaxZ,
                        position.Z
                    ),
                Y = 0,
                Z = 0
            },
            2 => new()
            {
                X = HierarchicalFunctionsPlus(
                        finiteElement.GetBounds().MinY,
                        finiteElement.GetBounds().MaxY,
                        position!.Y
                    )
                    * HierarchicalFunctionsMinus(
                        finiteElement.GetBounds().MinZ,
                        finiteElement.GetBounds().MaxZ,
                        position.Z
                    ),
                Y = 0,
                Z = 0
            },
            3 => new()
            {
                X = HierarchicalFunctionsMinus(
                        finiteElement.GetBounds().MinY,
                        finiteElement.GetBounds().MaxY,
                        position!.Y
                    )
                    * HierarchicalFunctionsPlus(
                        finiteElement.GetBounds().MinZ,
                        finiteElement.GetBounds().MaxZ,
                        position.Z
                    ),
                Y = 0,
                Z = 0
            },
            4 => new()
            {
                X = HierarchicalFunctionsPlus(
                        finiteElement.GetBounds().MinY,
                        finiteElement.GetBounds().MaxY,
                        position!.Y
                    )
                    * HierarchicalFunctionsPlus(
                        finiteElement.GetBounds().MinZ,
                        finiteElement.GetBounds().MaxZ,
                        position.Z
                    ),
                Y = 0,
                Z = 0
            },
            5 => new()
            {
                X = 0,
                Y = HierarchicalFunctionsMinus(
                        finiteElement.GetBounds().MinX,
                        finiteElement.GetBounds().MaxX,
                        position!.X
                    )
                    * HierarchicalFunctionsMinus(
                        finiteElement.GetBounds().MinZ,
                        finiteElement.GetBounds().MaxZ,
                        position.Z
                    ),
                Z = 0
            },
            6 => new()
            {
                X = 0,
                Y = HierarchicalFunctionsPlus(
                        finiteElement.GetBounds().MinX,
                        finiteElement.GetBounds().MaxX,
                        position!.X
                    )
                    * HierarchicalFunctionsMinus(
                        finiteElement.GetBounds().MinZ,
                        finiteElement.GetBounds().MaxZ,
                        position.Z
                    ),
                Z = 0
            },
            7 => new()
            {
                X = 0,
                Y = HierarchicalFunctionsMinus(
                        finiteElement.GetBounds().MinX,
                        finiteElement.GetBounds().MaxX,
                        position!.X
                    )
                    * HierarchicalFunctionsPlus(
                        finiteElement.GetBounds().MinZ,
                        finiteElement.GetBounds().MaxZ,
                        position.Z
                    ),
                Z = 0
            },
            8 => new()
            {
                X = 0,
                Y = HierarchicalFunctionsPlus(
                        finiteElement.GetBounds().MinX,
                        finiteElement.GetBounds().MaxX,
                        position!.X
                    )
                    * HierarchicalFunctionsPlus(
                        finiteElement.GetBounds().MinZ,
                        finiteElement.GetBounds().MaxZ,
                        position.Z
                    ),
                Z = 0
            },
            9 => new()
            {
                X = 0,
                Y = 0,
                Z = HierarchicalFunctionsMinus(
                        finiteElement.GetBounds().MinX,
                        finiteElement.GetBounds().MaxX,
                        position!.X
                    )
                    * HierarchicalFunctionsMinus(
                        finiteElement.GetBounds().MinY,
                        finiteElement.GetBounds().MaxY,
                        position.Y
                    )
            },
            10 => new()
            {
                X = 0,
                Y = 0,
                Z = HierarchicalFunctionsPlus(
                        finiteElement.GetBounds().MinX,
                        finiteElement.GetBounds().MaxX,
                        position!.X
                    )
                    * HierarchicalFunctionsMinus(
                        finiteElement.GetBounds().MinY,
                        finiteElement.GetBounds().MaxY,
                        position.Y
                    )
            },
            11 => new()
            {
                X = 0,
                Y = 0,
                Z = HierarchicalFunctionsMinus(
                        finiteElement.GetBounds().MinX,
                        finiteElement.GetBounds().MaxX,
                        position!.X
                    )
                    * HierarchicalFunctionsPlus(
                        finiteElement.GetBounds().MinY,
                        finiteElement.GetBounds().MaxY,
                        position.Y
                    )
            },
            12 => new()
            {
                X = 0,
                Y = 0,
                Z = HierarchicalFunctionsPlus(
                        finiteElement.GetBounds().MinX,
                        finiteElement.GetBounds().MaxX,
                        position!.X
                    )
                    * HierarchicalFunctionsPlus(
                        finiteElement.GetBounds().MinY,
                        finiteElement.GetBounds().MaxY,
                        position.Y
                    )
            },
            _ => throw new ArgumentOutOfRangeException($"{number}")
        };
    }

    private static double HierarchicalFunctionsMinus(double startPoint, double endPoint, double position) =>
        (endPoint - position) / (endPoint - startPoint);

    private static double HierarchicalFunctionsPlus(double startPoint, double endPoint, double position) =>
        (position - startPoint) / (endPoint - startPoint);
}
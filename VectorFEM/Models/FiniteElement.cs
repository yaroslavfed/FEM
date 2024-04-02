namespace VectorFEM.Models;

public record FiniteElement(
    double X0,
    double Xn,
    double Y0,
    double Yn,
    double Z0,
    double Zn,
    IList<Edge> Edges
);
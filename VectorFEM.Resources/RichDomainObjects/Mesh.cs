using System.Collections;
using VectorFEM.Data;

namespace VectorFEM.Resources.RichDomainObjects;

public class Mesh
{
    private IEnumerable<Node> _strata;
    private IEnumerable<Node> _points;
    private IEnumerable<double> _x, _y, _z;

    public int Nx { get; set; }
    public int Ny { get; set; }
    public int Nz { get; set; }

    public IEnumerable<Node> StrataMesh { get; set; } = [];
}
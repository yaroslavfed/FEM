using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEM.TerminalGui.Data;

public class MeshBounds
{
    public double StartX { get; set; }

    public double EndX { get; set; }

    public double StartY { get; set; }

    public double EndY { get; set; }

    public double StartZ { get; set; }

    public double EndZ { get; set; }

    public override string ToString()
    {
        return $"[{StartX}, {EndX}] x [{StartY}, {EndY}] x [{StartZ}, {EndZ}]";
    }
}
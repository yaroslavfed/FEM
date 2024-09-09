using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Client.Shared.Data;

public class AdditionParameters
{
    public double MuCoefficient { get; set; }

    public double GammaCoefficient { get; set; }

    public int BoundaryCondition { get; set; }
}
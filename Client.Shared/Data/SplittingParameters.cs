using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Client.Shared.Data;

public class SplittingParameters
{
    public double XSplittingCoefficient { get; set; }

    public double YSplittingCoefficient { get; set; }

    public double ZSplittingCoefficient { get; set; }

    public double XMultiplyCoefficient { get; set; }

    public double YMultiplyCoefficient { get; set; }

    public double ZMultiplyCoefficient { get; set; }
}
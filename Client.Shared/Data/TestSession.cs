using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Shared.Data;

public class TestSession
{
    public required Guid Id { get; set; }

    public required MeshParameters MeshParameters { get; set; }

    public required SplittingParameters SplittingParameters { get; set; }

    public required AdditionParameters AdditionParameters { get; set; }
}
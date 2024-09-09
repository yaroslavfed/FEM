using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Shared.Data;

public class TestSession
{
    public Guid Id { get; set; }

    public MeshParameters MeshParameters { get; set; }

    public SplittingParameters SplittingParameters { get; set; }

    public AdditionParameters AdditionParameters { get; set; }
}
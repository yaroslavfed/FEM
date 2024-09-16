namespace FEM.Server.Data.Domain;

public class TestSession
{
    public required Guid Id { get; set; }

    public required MeshParameters MeshParameters { get; set; }

    public required SplittingParameters SplittingParameters { get; set; }

    public required AdditionParameters AdditionParameters { get; set; }
}
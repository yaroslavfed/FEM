using FEM.Common.Data.Domain;

namespace FEM.Server.Data.Domain;

public record TestSession
{
    public required Guid Id { get; set; }

    public required MeshParameters MeshParameters { get; set; }

    public required SplittingParameters SplittingParameters { get; set; }

    public required AdditionParameters AdditionParameters { get; set; }

    public required IReadOnlyList<Strata> StrataList { get; set; }

    public double DensityBase { get; set; } = 0;
}
using FEM.Common.Data.Domain;

namespace FEM.Server.Data.Domain;

public record TestSession
{
    public required Guid Id { get; init; }

    public required MeshParameters MeshParameters { get; init; }

    public required SplittingParameters SplittingParameters { get; init; }

    public required AdditionParameters AdditionParameters { get; init; }

    public required IReadOnlyList<Strata> StrataList { get; init; }

    public required CurrentSource CurrentSource { get; init; }
}
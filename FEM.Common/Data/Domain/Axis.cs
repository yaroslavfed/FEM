using FEM.Common.Data.InputModels;

namespace FEM.Common.Data.Domain;

public record Axis
{
    public required AdditionalParameters AdditionalParameters { get; init; }

    public required Positioning Positioning { get; init; }

    public required Splitting Splitting { get; init; }

    public required TestingSettings TestingSettings { get; init; }
}
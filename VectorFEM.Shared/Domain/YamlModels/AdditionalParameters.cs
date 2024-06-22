namespace VectorFEM.Shared.Domain.YamlModels;

public record AdditionalParameters
{
    public int Mu { get; init; }
    public int Gamma { get; init; }
}
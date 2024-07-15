namespace FEM.Storage.FileStorage.YamlStorage.Data;

public record AdditionalParameters
{
    public int Mu { get; init; }

    public int Gamma { get; init; }
}
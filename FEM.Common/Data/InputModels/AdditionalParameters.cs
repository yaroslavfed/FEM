namespace FEM.Common.Data.InputModels;

public abstract record AdditionalParameters
{
    public int Mu { get; init; }

    public int Gamma { get; init; }
}
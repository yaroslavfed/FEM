namespace Client.Avalonia.Components.Tabs.AdditionalParametersTab;

public record BoundaryCondition
{
    public required int BoundaryNumber { get; init; }

    public required string BoundaryName { get; init; }

    public bool IsBoundarySelected { get; set; }
}
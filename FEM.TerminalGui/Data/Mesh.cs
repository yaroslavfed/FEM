namespace FEM.TerminalGui.Data;
public record Mesh
{
    public double XCenterCoordinate { get; set; }

    public double YCenterCoordinate { get; set; }

    public double ZCenterCoordinate { get; set; }

    public double XStepToBounds { get; set; }

    public double YStepToBounds { get; set; }

    public double ZStepToBounds { get; set; }
}

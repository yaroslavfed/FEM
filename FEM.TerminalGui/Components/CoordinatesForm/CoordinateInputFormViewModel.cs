using System.Reactive.Linq;
using System.Runtime.Serialization;
using FEM.TerminalGui.Data;
using NStack;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace FEM.TerminalGui.Components.CoordinatesForm;

[DataContract]
public class CoordinateInputFormViewModel : ViewModelBase
{
    #region Labels

    public string CenterCoordinatesLabel { get; } = "Центр фигуры:";
    public string StepToBoundsLabel { get; } = "Расстояние до границ:";

    #endregion

    #region Properties

    [Reactive]
    public Mesh Mesh { get; set; } = new();

    [Reactive, DataMember]
    public ustring XCenterCoordinate { get; set; } = ustring.Empty;

    [Reactive, DataMember]
    public ustring YCenterCoordinate { get; set; } = ustring.Empty;

    [Reactive, DataMember]
    public ustring ZCenterCoordinate { get; set; } = ustring.Empty;

    [Reactive, DataMember]
    public ustring XStepToBounds { get; set; } = ustring.Empty;

    [Reactive, DataMember]
    public ustring YStepToBounds { get; set; } = ustring.Empty;

    [Reactive, DataMember]
    public ustring ZStepToBounds { get; set; } = ustring.Empty;

    [Reactive]
    public MeshBounds MeshBounds { get; set; } = new();

    #endregion
}
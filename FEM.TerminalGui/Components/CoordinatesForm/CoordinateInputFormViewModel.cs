using System.Runtime.Serialization;
using NStack;
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

    [Reactive, DataMember]
    public ustring XCenterCoordinate { get; set; } = "0";

    [Reactive, DataMember]
    public ustring YCenterCoordinate { get; set; } = "0";

    [Reactive, DataMember]
    public ustring ZCenterCoordinate { get; set; } = "0";

    [Reactive, DataMember]
    public ustring XStepToBounds { get; set; } = "1";

    [Reactive, DataMember]
    public ustring YStepToBounds { get; set; } = "1";

    [Reactive, DataMember]
    public ustring ZStepToBounds { get; set; } = "1";

    #endregion
}
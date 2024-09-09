using System.Runtime.Serialization;
using NStack;
using ReactiveUI.Fody.Helpers;

namespace FEM.TerminalGui.Components.AdditionalParamsForm;

[DataContract]
public class AdditionalParamsFormViewModel : ViewModelBase
{
    #region Properties

    [Reactive, DataMember]
    public ustring MuCoefficient { get; set; } = "0";

    [Reactive, DataMember]
    public ustring GammaCoefficient { get; set; } = "0";

    [Reactive]
    public int BoundaryCondition { get; set; }

    #endregion
}
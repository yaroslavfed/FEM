using System.Runtime.Serialization;
using NStack;
using ReactiveUI.Fody.Helpers;

namespace FEM.TerminalGui.Components.SplittingForm;

[DataContract]
public class SplittingInputFormViewModel : ViewModelBase
{
    #region Labels

    public string SplittingCoefficientLabel { get; } = "Параметры дробления:";
    public string MultiplyCoefficientLabel { get; } = "Коэффициент разрядки:";

    #endregion

    #region Properties

    [Reactive, DataMember]
    public ustring XSplittingCoefficient { get; set; } = "1";

    [Reactive, DataMember]
    public ustring YSplittingCoefficient { get; set; } = "1";

    [Reactive, DataMember]
    public ustring ZSplittingCoefficient { get; set; } = "1";

    [Reactive, DataMember]
    public ustring XMultiplyCoefficient { get; set; } = "0";

    [Reactive, DataMember]
    public ustring YMultiplyCoefficient { get; set; } = "0";

    [Reactive, DataMember]
    public ustring ZMultiplyCoefficient { get; set; } = "0";

    #endregion
}
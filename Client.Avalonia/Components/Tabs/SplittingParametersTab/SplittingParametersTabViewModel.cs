using Client.Avalonia.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace Client.Avalonia.Components.Tabs.SplittingParametersTab;

public class SplittingParametersTabViewModel : ViewModelBase
{

    #region Labels

    public string SplittingParamsLabel { get; set; } = "Кол-во конечных элементов:";

    public string MultyParamsLabel { get; set; } = "Коэффициент разрядки:";

    #endregion

    #region Properties

    [Reactive]
    public double XSplittingCoefficient { get; set; } = 1;

    [Reactive]
    public double YSplittingCoefficient { get; set; } = 1;

    [Reactive]
    public double ZSplittingCoefficient { get; set; } = 1;

    [Reactive]
    public double XMultiplyCoefficient { get; set; } = 1;

    [Reactive]
    public double YMultiplyCoefficient { get; set; } = 1;

    [Reactive]
    public double ZMultiplyCoefficient { get; set; } = 1;

    #endregion

}
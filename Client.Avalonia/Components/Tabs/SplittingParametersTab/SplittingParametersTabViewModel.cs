using Client.Avalonia.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace Client.Avalonia.Components.Tabs.SplittingParametersTab;

public class SplittingParametersTabViewModel : ViewModelBase
{

    #region Labels



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
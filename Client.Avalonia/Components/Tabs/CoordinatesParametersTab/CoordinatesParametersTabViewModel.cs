using Client.Avalonia.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace Client.Avalonia.Components.Tabs.CoordinatesParametersTab;

public class CoordinatesParametersTabViewModel : ViewModelBase
{

    #region Labels

    public string ElementCenterLabel { get; set; } = "Центр фигуры:";

    public string ElementBoundsLabel { get; set; } = "Расстояние до границ:";

    #endregion

    #region Properties

    [Reactive]
    public double XCenterCoordinate { get; set; }

    [Reactive]
    public double YCenterCoordinate { get; set; }

    [Reactive]
    public double ZCenterCoordinate { get; set; }

    [Reactive]
    public double XStepToBounds { get; set; } = 1;

    [Reactive]
    public double YStepToBounds { get; set; } = 1;

    [Reactive]
    public double ZStepToBounds { get; set; } = 1;

    #endregion

}
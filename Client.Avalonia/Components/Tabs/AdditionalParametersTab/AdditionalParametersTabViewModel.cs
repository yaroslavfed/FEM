using System.Collections.Generic;
using Client.Avalonia.ViewModels;
using ReactiveUI.Fody.Helpers;

namespace Client.Avalonia.Components.Tabs.AdditionalParametersTab;

public class AdditionalParametersTabViewModel : ViewModelBase
{

    #region Labels

    public string MuLabel { get; set; } = "Мю:";

    public string GammaLabel { get; set; } = "Гамма:";

    public string BoundaryConditionsGroupLabel { get; set; } = "Краевое условие:";

    #endregion

    #region Properties

    [Reactive]
    public double MuCoefficient { get; set; } = 1;

    [Reactive]
    public double GammaCoefficient { get; set; } = 1;

    public List<BoundaryCondition> BoundaryConditions { get; } =
    [
        new() { BoundaryNumber = 0, BoundaryName = "Первое краевое условие", IsBoundarySelected = true},
        new() { BoundaryNumber = 1, BoundaryName = "Второе краевое условие" },
        new() { BoundaryNumber = 2, BoundaryName = "Третье краевое условие" }
    ];

    #endregion

}
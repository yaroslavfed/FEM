using Client.Avalonia.Pages.AdditionalParametersTab;
using Client.Avalonia.Pages.CoordinatesParametersTab;
using Client.Avalonia.Pages.SplittingParametersTab;
using Client.Avalonia.ViewModels;
using ReactiveUI;

namespace Client.Avalonia.Windows.Main;

public class MainWindowViewModel : ViewModelBase, IScreen
{
    public MainWindowViewModel(
        CoordinatesParametersTabViewModel coordinatesParametersTabViewModel,
        SplittingParametersTabViewModel splittingParametersTabViewModel,
        AdditionalParametersTabViewModel additionalParametersTabViewModel
    )
    {
        CoordinatesParametersTabViewModel = coordinatesParametersTabViewModel;
        SplittingParametersTabViewModel = splittingParametersTabViewModel;
        AdditionalParametersTabViewModel = additionalParametersTabViewModel;
    }

    #region Properties

    public RoutingState Router { get; set; } = new();

    public CoordinatesParametersTabViewModel CoordinatesParametersTabViewModel { get; }

    public SplittingParametersTabViewModel SplittingParametersTabViewModel { get; }

    public AdditionalParametersTabViewModel AdditionalParametersTabViewModel { get; }

    #endregion

}
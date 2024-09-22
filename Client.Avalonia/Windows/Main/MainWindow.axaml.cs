using Avalonia.ReactiveUI;

namespace Client.Avalonia.Windows.Main;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}
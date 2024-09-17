using Autofac;
using Client.Avalonia.Pages.AdditionalParametersTab;
using Client.Avalonia.Pages.CoordinatesParametersTab;
using Client.Avalonia.Pages.SplittingParametersTab;
using Client.Avalonia.Windows.Main;
using ReactiveUI;

namespace Client.Avalonia.Installers;

static internal class ViewModelInstaller
{
    public static void RegisterViewModels(this ContainerBuilder builder)
    {
        builder.RegisterType<MainWindowViewModel>().AsSelf().As<IScreen>().SingleInstance();
        builder.RegisterType<CoordinatesParametersTabViewModel>();
        builder.RegisterType<SplittingParametersTabViewModel>();
        builder.RegisterType<AdditionalParametersTabViewModel>();
    }
}
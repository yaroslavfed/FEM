using Autofac;
using Client.Avalonia.Installers;
using Client.Avalonia.ViewModels;

namespace Client.Avalonia;

internal class Bootstrapper : BootstrapperBase<Bootstrapper>
{
    protected override void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterServices();
        builder.RegisterAutoMapperConfiguration();
        builder.RegisterAutoMapper();
    }

    protected override void RegisterViewModels(ContainerBuilder builder)
    {
        builder.RegisterViewModels();
    }
}
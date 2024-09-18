using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Client.Avalonia.Extensions;
using Client.Avalonia.Windows.Main;
using Client.Shared.Services;
using ReactiveUI;
using Splat;

namespace Client.Avalonia;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        SetupMainWindow();

        // Поднимаем сервер вместе с фронтом 
        StartBackend();
        base.OnFrameworkInitializationCompleted();
    }

    public override void RegisterServices()
    {
        base.RegisterServices();
        Bootstrapper.Instance.BuildIoC();
    }

    private void SetupMainWindow()
    {
        if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop) return;

        MainWindow window = new() { DataContext = Locator.Current.GetService<IScreen>() };

        window.Show();
        desktop.MainWindow = window;
        desktop.Exit += DesktopOnExit;
        desktop.ShutdownRequested += DesktopOnExit;
    }


    private static void StartBackend()
    {
        var serverInitializer = Locator.Current.GetService<ServerInitializer>();
        serverInitializer!.Start();
    }

    private static void DesktopOnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        Locator.Current.GetRequiredService<ServerInitializer>().Kill();
    }

    private void DesktopOnExit(object? sender, ShutdownRequestedEventArgs e)
    {
        Locator.Current.GetRequiredService<ServerInitializer>().Kill();
    }

}
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Client.Avalonia.Extensions;
using Client.Avalonia.Windows.Main;
using Client.Shared.Services.BackendLifeTimeManager;
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
#if !DEBUG
        StartBackend();
#endif
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

        Dispatcher.UIThread.Post(
            () =>
            {
                MainWindow window = new() { DataContext = Locator.Current.GetService<IScreen>() };

                window.Show();
                desktop.MainWindow = window;
                desktop.Exit += DesktopOnExit;
                desktop.ShutdownRequested += DesktopOnExit;
            },
            DispatcherPriority.Background
        );
    }

    private static void StartBackend()
    {
        var serverInitializer = Locator.Current.GetRequiredService<IServerInitializer>();
        serverInitializer.Start();
    }

    private static void DesktopOnExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        Locator.Current.GetRequiredService<IServerInitializer>().Kill();
    }

    private void DesktopOnExit(object? sender, ShutdownRequestedEventArgs e)
    {
        Locator.Current.GetRequiredService<IServerInitializer>().Kill();
    }
}
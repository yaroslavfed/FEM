using System;
using Avalonia;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;
using Splat.ModeDetection;

namespace Client.Avalonia;

internal sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        ModeDetector.OverrideModeDetector(Mode.Run);
        RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;

        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
                     .UsePlatformDetect()
                     .WithInterFont()
                     .LogToTrace()
                     .UseReactiveUI();
}
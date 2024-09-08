using System.Reactive.Concurrency;
using FEM.TerminalGui;
using FEM.TerminalGui.Windows.MainWindow;
using ReactiveUI;
using Terminal.Gui;

Application.Init();
RxApp.MainThreadScheduler = TerminalScheduler.Default;
RxApp.TaskpoolScheduler = TaskPoolScheduler.Default;

Application.Run(new MainWindow(new MainWindowViewModel()));
Application.Shutdown();
using System.Reactive.Concurrency;
using AutoMapper;
using Client.Shared.API;
using Client.Shared.Installers;
using Client.Shared.Services.TestingService;
using FEM.TerminalGui;
using FEM.TerminalGui.Windows.MainWindow;
using ReactiveUI;
using Terminal.Gui;

Application.Init();
RxApp.MainThreadScheduler = TerminalScheduler.Default;
RxApp.TaskpoolScheduler = TaskPoolScheduler.Default;

IConfigurationProvider autoMapperConfiguration = AutoMapperConfigurationInstaller.RegisterAutoMapperConfiguration();
IMapper mapper = new Mapper(autoMapperConfiguration);
ITestingServiceClient testingServiceClient = new TestingServiceClient(mapper);

ITestingService testingService = new TestingService(mapper, testingServiceClient);

Application.Run(new MainWindow(new MainWindowViewModel(testingService)));
Application.Shutdown();
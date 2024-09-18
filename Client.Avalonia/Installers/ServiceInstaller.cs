using Autofac;
using Client.Shared.API.TestingServiceClient;
using Client.Shared.Services;
using Client.Shared.Services.ReportService;
using Client.Shared.Services.TestingService;

namespace Client.Avalonia.Installers;

static internal class ServiceInstaller
{
    public static void RegisterServices(this ContainerBuilder builder)
    {
        builder.RegisterType<ServerInitializer>().SingleInstance();

        builder.RegisterType<PdfReportService>().As<IReportService>();
        builder.RegisterType<TestingService>().As<ITestingService>();
        builder.RegisterType<TestingServiceClient>().As<ITestingServiceClient>();
    }
}
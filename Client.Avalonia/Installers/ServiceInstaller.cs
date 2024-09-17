using Autofac;
using Client.Shared.Services.ReportService;

namespace Client.Avalonia.Installers;

internal static class ServiceInstaller
{
    public static void RegisterServices(this ContainerBuilder builder)
    {
        builder.RegisterType<PdfReportService>().As<IReportService>();
    }
}
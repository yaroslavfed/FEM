using System.Diagnostics.CodeAnalysis;
using Autofac;
using FEM.Core.Installers;

namespace FEM.Core;

[ExcludeFromCodeCoverage]
internal abstract class Program
{
    private static IContainer ContainerRoot()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<Startup>();

        builder.RegisterAutoMapperConfiguration();
        builder.RegisterAutoMapper();
        builder.RegisterAutofac();
        builder.RegisterServices();
        builder.RegisterStorages();

        return builder.Build();
    }

    public static async Task Main(string[] args) => await ContainerRoot().Resolve<Startup>().Run();
}
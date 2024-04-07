using System.Diagnostics.CodeAnalysis;
using Autofac;
using VectorFEM.Installers;

namespace VectorFEM;

[ExcludeFromCodeCoverage]
internal abstract class Program
{
    private static IContainer ContainerRoot()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<Startup>();

        builder.RegisterServices();
        builder.RegisterAutofac();

        return builder.Build();
    }

    public static async Task Main(string[] args)
    {
        await ContainerRoot().Resolve<Startup>().Run();
    }
}
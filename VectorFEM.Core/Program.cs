using System.Diagnostics.CodeAnalysis;
using Autofac;
using VectorFEM.Core.Installers;

namespace VectorFEM.Core;

[ExcludeFromCodeCoverage]
internal abstract class Program
{
    private static IContainer ContainerRoot()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<Startup>();

        builder.RegisterAutofac();
        builder.RegisterResolvers();
        builder.RegisterServices();
        builder.RegisterParsers();

        return builder.Build();
    }

    public static async Task Main(string[] args)
    {
        await ContainerRoot().Resolve<Startup>().Run();
    }
}
using Autofac;

namespace VectorFEM;

internal class Program
{
    private static IContainer ContainerRoot()
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<Startup>();

        // registers

        return builder.Build();
    }

    public static async Task Main(string[] args)
    {
        await ContainerRoot().Resolve<Startup>().Run();
    }
}

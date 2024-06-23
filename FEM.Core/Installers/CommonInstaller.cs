using Autofac;
using Splat;
using Splat.Autofac;

namespace FEM.Core.Installers;

public static class CommonInstaller
{
    public static void RegisterAutofac(this ContainerBuilder builder)
    {
        var resolver = builder.UseAutofacDependencyResolver();
        resolver.InitializeSplat();
        builder.RegisterInstance(resolver);
    }
}
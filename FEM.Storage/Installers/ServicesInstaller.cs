using Autofac;
using FEM.Common.Parsers;
using FEM.Storage.Converter;
using FEM.Storage.FileStorage.YamlStorage;

namespace FEM.Storage.Installers;

public static class ServicesInstaller
{
    public static void RegisterServices(ContainerBuilder builder)
    {
        builder.RegisterType<YamlConverter>().As<IConverter>();
        builder.RegisterType<YamlParser>().As<IParser>();
    }
}
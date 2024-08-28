using Autofac;
using FEM.Common.Parsers;
using FEM.Storage.Converter;
using FEM.Storage.FileStorage.JsonStorage;

namespace FEM.Storage.Installers;

public static class ServicesInstaller
{
    public static void RegisterStorageServices(ContainerBuilder builder)
    {
        builder.RegisterType<ConverterService>().As<IConverterService>();
        builder.RegisterType<JsonParser>().As<IParser>();
    }
}
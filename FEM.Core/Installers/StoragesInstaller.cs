using Autofac;
using FEM.Common.Parsers;
using FEM.Storage.FileStorage.JsonStorage;
using FEM.Storage.FileStorage;

namespace FEM.Core.Installers;

public static class StoragesInstaller
{
    public static void RegisterStorages(this ContainerBuilder builder)
    {
        builder.RegisterType<JsonParser>().As<IParser>();
        builder.RegisterType<JsonStorage>().As<IReadableStorage>();
    }
}
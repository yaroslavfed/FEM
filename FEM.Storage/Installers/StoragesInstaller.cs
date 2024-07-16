using Autofac;
using FEM.Storage.FileStorage;
using FEM.Storage.FileStorage.JsonStorage;

namespace FEM.Storage.Installers;

public static class StoragesInstaller
{
    public static void RegisterStorages(ContainerBuilder builder) =>
        builder.RegisterType<JsonStorage>().As<IReadableStorage>();
}
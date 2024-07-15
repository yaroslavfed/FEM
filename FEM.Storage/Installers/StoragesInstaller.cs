using Autofac;
using FEM.Core.Storages;
using FEM.Storage.FileStorage.YamlStorage;

namespace FEM.Storage.Installers;

public static class StoragesInstaller
{
    public static void RegisterStorages(ContainerBuilder builder)
    {
        builder.RegisterType<YamlStorage>().As<IReadableStorage>();
    }
}
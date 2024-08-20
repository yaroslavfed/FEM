using Autofac;
using FEM.Storage.FileStorage;
using FEM.Storage.FileStorage.JsonStorage;

namespace FEM.Storage.Installers;

public static class RepositoriesInstaller
{
    public static void RegisterStorageRepositories(ContainerBuilder builder) =>
        builder.RegisterType<JsonStorage>().As<IReadableStorage>();
}
using Autofac;

namespace FEM.Core.Installers;

public static class StoragesInstaller
{
    public static void RegisterStorages(this ContainerBuilder builder)
    {
        Storage.Installers.StoragesInstaller.RegisterStorages(builder);
    }
}
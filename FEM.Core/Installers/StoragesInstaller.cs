using Autofac;
using FEM.Core.Storages;
using FEM.Core.Storages.YamlStorages.TestConfigurationStorage;
using FEM.Core.Storages.YamlStorages.YamlMeshStorage;
using FEM.Shared.Domain.YamlModels.YamlMeshBuilder;

namespace FEM.Core.Installers;

public static class StoragesInstaller
{
    public static void RegisterStorages(this ContainerBuilder builder)
    {
        builder.RegisterType<YamlMeshStorage>().As<IReadable<YamlMesh>>();
        builder.RegisterType<YamlTestConfigurationStorage>().As<IReadable<YamlTestSettings>>();
    }
}
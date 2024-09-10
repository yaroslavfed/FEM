using Autofac;
using FEM.Common.Parsers;
using FEM.Storage.FileStorage;
using FEM.Storage.FileStorage.JsonStorage;

namespace FEM.Server.Installers;

public static class StoragesInstaller
{
    public static void AddStorages(this IServiceCollection builder)
    {
        builder.AddScoped<IParser, JsonParser>();
        builder.AddScoped<IReadableStorage, JsonStorage>();
    }
}
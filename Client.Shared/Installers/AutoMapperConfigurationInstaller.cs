using AutoMapper;
using System.Reflection;
using Client.Shared.Profiles;

namespace Client.Shared.Installers;

public static class AutoMapperConfigurationInstaller
{
    public static IConfigurationProvider RegisterAutoMapperConfiguration() =>
        new MapperConfiguration(ConfigureMapper);

    private static void ConfigureMapper(IMapperConfigurationExpression configuration)
    {
        configuration.AddMaps(Assembly.GetAssembly(typeof(TestSessionProfile)),
            Assembly.GetAssembly(typeof(TestSessionProfile)));
    }
}
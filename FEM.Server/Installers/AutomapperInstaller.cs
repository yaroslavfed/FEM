using System.Reflection;

namespace FEM.Server.Installers;

public static class AutomapperInstaller
{
    public static void AddAutoMapper(this IServiceCollection source)
    {
        source.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
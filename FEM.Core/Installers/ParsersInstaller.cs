using Autofac;
using FEM.Common.Parsers;

namespace FEM.Core.Installers;

public static class ParsersInstaller
{
    public static void RegisterParsers(this ContainerBuilder builder)
    {
        builder.RegisterType<ParserYaml>().As<IParser>();
    }
}
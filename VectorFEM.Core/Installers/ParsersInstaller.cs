using Autofac;
using VectorFEM.Common.Parsers;

namespace VectorFEM.Core.Installers;

public static class ParsersInstaller
{
    public static void RegisterParsers(this ContainerBuilder builder)
    {
        builder.RegisterType<ParserYaml>().As<IParser>();
    }
}
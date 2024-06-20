using Autofac;
using VectorFEM.GridBuilder.Parsers;
using VectorFEM.Resources.Data;

namespace VectorFEM.Core.Installers;

public static class ParsersInstaller
{
    public static void RegisterParsers(this ContainerBuilder builder)
    {
        builder.RegisterType<ParserYaml>().As<IParser>();
    }
}
using System.Collections.Frozen;
using VectorFEM.Common.Parsers;
using VectorFEM.Shared.Domain;
using VectorFEM.Shared.Domain.YamlModels;

namespace VectorFEM.GridBuilder.RichDomainObjects;

public class Mesh
{
    private readonly IParser _yamlParser;

    private IEnumerable<Node> _strata = Array.Empty<Node>();
    private IEnumerable<Node> _points = Array.Empty<Node>();
    private IEnumerable<double> _x, _y, _z;
    private double _kx, _ky, _kz;

    private readonly FrozenDictionary<string, string> _directories = new KeyValuePair<string, string>[]
    {
        new("estimatedArea", Path.Combine(Directory.GetCurrentDirectory(), "estimated_area.yaml")),
        new("positioning", Path.Combine(Directory.GetCurrentDirectory(), "positioning.yaml")),
        new("splitting", Path.Combine(Directory.GetCurrentDirectory(), "splitting.yaml")),
        new("additionalParameters", Path.Combine(Directory.GetCurrentDirectory(), "additional_parameters.yaml")),
        new("testingSettings", Path.Combine(Directory.GetCurrentDirectory(), "testing_settings.yaml"))
    }.ToFrozenDictionary();

    public Mesh(IParser yamlParser)
    {
        _yamlParser = yamlParser;
    }

    public int Nx { get; set; }
    public int Ny { get; set; }
    public int Nz { get; set; }

    public IEnumerable<Node> StrataMesh { get; set; } = [];

    public async Task ResolveMesh()
    {
        var positioning = await _yamlParser.ParseEntityFromFile<Positioning>(_directories["positioning"]);
        var splitting = await _yamlParser.ParseEntityFromFile<Splitting>(_directories["splitting"]);
        var additionalParameters = await _yamlParser.ParseEntityFromFile<AdditionalParameters>(_directories["additionalParameters"]);
        var testingSettings = await _yamlParser.ParseEntityFromFile<TestingSettings>(_directories["testingSettings"]);
    }
    //
    // private Task<IReadOnlyList<FiniteElement>> GenerateFiniteElements()
    // {
    //     return new Task<IReadOnlyList<FiniteElement>>();
    // }
}
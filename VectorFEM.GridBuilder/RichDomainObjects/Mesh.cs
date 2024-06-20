using System.Collections.Frozen;
using System.Text;
using VectorFEM.Data;
using VectorFEM.GridBuilder.Parsers;
using VectorFEM.Resources.Data.YamlModels;

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
            new("splitting", Path.Combine(Directory.GetCurrentDirectory(), "splitting.yaml")),
            new("additional", Path.Combine(Directory.GetCurrentDirectory(), "additional.yaml")),
            new("testingSettings", Path.Combine(Directory.GetCurrentDirectory(), "testing_settings.yaml"))
        }
        .ToFrozenDictionary();

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
        var estimatedArea = await GetParsedEntity<EstimatedArea>(_directories["estimatedArea"]);
        var splitting = await GetParsedEntity<Splitting>(_directories["splitting"]);
        var additional = await GetParsedEntity<Additional>(_directories["additional"]);
        var testingSettings = await GetParsedEntity<TestingSettings>(_directories["testingSettings"]);

        // TODO: перенести строитель КЭ
    }

    private async Task<TData> GetParsedEntity<TData>(string path)
    {
        using var streamReader = new StreamReader(path, Encoding.UTF8);
        var inputData = await streamReader.ReadToEndAsync();
        return await _yamlParser.DeserializeOutput<TData>(inputData);
    }
}
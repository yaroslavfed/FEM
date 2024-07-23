using System.Xml.Linq;
using FEM.Core.Services.MeshService;

namespace FEM.Core;

public class Startup
{
    private readonly IMeshService _meshService;

    public Startup(IMeshService meshService)
    {
        _meshService = meshService;
    }

    public async Task Run()
    {
        var mesh = await _meshService.GenerateMeshAsync();
    }
}
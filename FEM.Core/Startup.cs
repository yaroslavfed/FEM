using FEM.Common.Data.Domain;
using FEM.Core.Services.DrawingService;
using FEM.Core.Services.MeshService;

namespace FEM.Core;

public class Startup
{
    private readonly IMeshService          _meshService;
    private readonly IDrawingService<Mesh> _drawingService;

    public Startup(IMeshService meshService, IDrawingService<Mesh> drawingService)
    {
        _meshService = meshService;
        _drawingService = drawingService;
    }

    public async Task Run()
    {
        var mesh = await _meshService.GenerateMeshAsync();
        await _drawingService.StartDrawProcess(mesh);

        Console.ReadKey();
    }
}
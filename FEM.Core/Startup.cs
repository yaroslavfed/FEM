using FEM.Common.Data.Domain;
using VectorFEM.Core.Services.Parallelepipedal.DrawingMeshService;
using VectorFEM.Core.Services.Parallelepipedal.MeshService;

namespace FEM.Core;

public class Startup
{
    private readonly IMeshDrawingService _drawingService;
    private readonly IMeshService        _meshService;

    public Startup(IMeshDrawingService drawingService, IMeshService meshService)
    {
        _drawingService = drawingService;
        _meshService = meshService;
    }

    public async Task Run()
    {
        var mesh = await _meshService.GenerateMeshAsync();

#if DEBUG
        await _drawingService.StartDrawingProcess(mesh);
#endif
        Console.ReadKey();
    }
}
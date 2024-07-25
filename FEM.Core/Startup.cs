using FEM.Common.Data.Domain;
using FEM.Core.Services.DrawingService;
using FEM.Core.Services.MatrixServices.MatrixPortraitService;
using FEM.Core.Services.MeshService;

namespace FEM.Core;

public class Startup
{
    private readonly IMeshService                 _meshService;
    private readonly IDrawingService<Mesh>        _drawingService;
    private readonly IMatrixPortraitService<Mesh> _portraitService;

    public Startup(
        IMeshService meshService,
        IDrawingService<Mesh> drawingService,
        IMatrixPortraitService<Mesh> portraitService
    )
    {
        _meshService = meshService;
        _drawingService = drawingService;
        _portraitService = portraitService;
    }

    public async Task Run()
    {
        var mesh = await _meshService.GenerateMeshAsync();
        await _portraitService.ResolveMatrixPortrait(mesh);

#if DEBUG
        await _drawingService.StartDrawingProcess(mesh);
#endif
        Console.ReadKey();
    }
}
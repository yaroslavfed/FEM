using FEM.Core.Services.MeshService;

namespace FEM.Core;

public class Startup
{
    private readonly IMeshService _meshService;

    public Startup(IMeshService meshService)
    {
        _meshService = meshService;
    }

    public Task Run()
    {
        _meshService.GenerateMesh();

        return Task.CompletedTask;
    }
}
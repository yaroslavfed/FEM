using FEM.Common.Enums;
using VectorFEM.Core.Services.Parallelepipedal.GlobalMatrixService;

namespace FEM.Core;

public class Startup
{
    private readonly IGlobalMatrixServices _globalMatrixServices;

    public Startup(IGlobalMatrixServices globalMatrixServices)
    {
        _globalMatrixServices = globalMatrixServices;
    }

    public async Task Run()
    {
        await _globalMatrixServices.GetGlobalMatrixAsync(EMatrixFormats.Profile);

        Console.ReadKey();
    }
}
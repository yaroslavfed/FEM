using FEM.Common.RichDomainObjects;
using FEM.Core.Services.MatrixServices.GlobalMatrixServices;

namespace FEM.Core;

public class Startup(
    IGlobalMatrixServices globalMatrixServices,
    Mesh mesh
)
{
    public async Task Run()
    {
        await mesh.ResolveMesh();
    }
}
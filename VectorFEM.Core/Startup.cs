using VectorFEM.Core.Services;
using VectorFEM.Resources.RichDomainObjects;

namespace VectorFEM.Core;

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
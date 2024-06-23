﻿using VectorFEM.Core.Services.MatrixServices.GlobalMatrixServices;
using VectorFEM.GridBuilder.RichDomainObjects;

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
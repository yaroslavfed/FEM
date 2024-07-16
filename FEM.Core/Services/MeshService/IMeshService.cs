using FEM.Common.Data.Domain;

namespace FEM.Core.Services.MeshService;

public interface IMeshService
{
    Task<Mesh> GenerateMesh();
}
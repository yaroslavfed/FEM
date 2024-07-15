using FEM.Shared.Domain.Data;

namespace FEM.Core.Services.MeshService;

public interface IMeshService
{
    Task<Mesh> GenerateMesh();
}
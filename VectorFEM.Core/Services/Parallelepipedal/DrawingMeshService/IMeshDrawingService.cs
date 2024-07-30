using VectorFEM.Core.Data.Parallelepipedal;

namespace VectorFEM.Core.Services.Parallelepipedal.DrawingMeshService;

public interface IMeshDrawingService
{
    Task StartDrawingProcess(Mesh mesh);
}
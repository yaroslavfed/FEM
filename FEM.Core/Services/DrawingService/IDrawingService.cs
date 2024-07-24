namespace FEM.Core.Services.DrawingService;

public interface IDrawingService<in TData>
{
    Task StartDrawingProcess(TData mesh);
}
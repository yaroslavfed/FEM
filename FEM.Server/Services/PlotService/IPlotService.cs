using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.PlotService;

public interface IPlotService
{
    Task ShowPlotAsync(Mesh mesh);
}
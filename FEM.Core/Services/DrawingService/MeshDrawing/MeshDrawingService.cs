using System.Diagnostics;
using FEM.Common.Data.Domain;

namespace FEM.Core.Services.DrawingService.MeshDrawing;

public class MeshDrawingService<TData> : IDrawingService<TData> where TData : Mesh
{
    private readonly string _dataFileName = Path.Combine(Directory.GetCurrentDirectory(), "Scripts\\output.txt");
    private readonly string _scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts\\draw_mesh_script.py");

    public async Task StartDrawProcess(TData mesh)
    {
        var isScriptFileExist = await CheckFilesToAvailabilityAsync(_scriptPath);
        if (!isScriptFileExist)
            throw new FileNotFoundException($"Script file was not found from path {_scriptPath}");

        await ResolveDataToDrawAsync(mesh);

        var isDataFileExist = await CheckFilesToAvailabilityAsync(_dataFileName);
        if (!isDataFileExist)
            throw new FileNotFoundException($"Data file was not found from path {_dataFileName}");

        await RunScriptAsync();
    }

    private Task RunScriptAsync()
    {
        using Process myProcess = new();
        myProcess.StartInfo.FileName = "python";
        myProcess.StartInfo.Arguments = _scriptPath;
        myProcess.StartInfo.UseShellExecute = false;
        myProcess.StartInfo.RedirectStandardInput = true;
        myProcess.StartInfo.RedirectStandardOutput = false;
        myProcess.Start();

        return Task.CompletedTask;
    }

    private async Task ResolveDataToDrawAsync(Mesh mesh)
    {
        var sw = new StreamWriter(_dataFileName);
        await sw.WriteLineAsync($"{mesh.Elements.Count}");

        foreach (var fe in mesh.Elements)
        {
            var pointsX = fe.Edges.SelectMany(edge => edge.Nodes)
                            .Select(node => node.Coordinate.X)
                            .Distinct()
                            .Order()
                            .ToList();

            var pointsY = fe.Edges.SelectMany(edge => edge.Nodes)
                            .Select(node => node.Coordinate.Y)
                            .Distinct()
                            .Order()
                            .ToList();

            var pointsZ = fe.Edges.SelectMany(edge => edge.Nodes)
                            .Select(node => node.Coordinate.Z)
                            .Distinct()
                            .Order()
                            .ToList();

            await sw.WriteAsync(pointsX[0] + " ");
            await sw.WriteAsync(pointsX[1] + " ");
            await sw.WriteAsync(pointsY[0] + " ");
            await sw.WriteAsync(pointsY[1] + " ");
            await sw.WriteAsync(pointsZ[0] + " ");
            await sw.WriteAsync(pointsZ[1] + " ");
            await sw.WriteLineAsync();
        }

        sw.Close();
    }

    private static Task<bool> CheckFilesToAvailabilityAsync(string pathToFile) =>
        Task.FromResult(File.Exists(pathToFile));
}
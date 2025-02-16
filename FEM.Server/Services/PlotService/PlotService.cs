using System.Diagnostics;
using FEM.Server.Data.Parallelepipedal;
using Newtonsoft.Json;

namespace FEM.Server.Services.PlotService;

public class PlotService : IPlotService
{
    private Task CreateDataFiles(Mesh mesh)
    {
        var json = JsonConvert.SerializeObject(mesh, Formatting.Indented);
        File.WriteAllText("mesh_data.json", json);

        Console.WriteLine("Data saved to mesh_data.json");

        var outputPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "mesh_data.json"
        );

        var watcher = new FileSystemWatcher
        {
            Path = Path.GetDirectoryName(outputPath) ?? throw new InvalidOperationException(),
            Filter = Path.GetFileName(outputPath),
            EnableRaisingEvents = true
        };

        watcher.Changed += (s, e) => Console.WriteLine("Data updated - ready for visualization");

        return Task.CompletedTask;
    }

    public async Task ShowPlotAsync(Mesh mesh)
    {
        await CreateDataFiles(mesh);

        using Process myProcess = new();
        myProcess.StartInfo.FileName = "python";
        myProcess.StartInfo.Arguments = @"Scripts/show_plots_script.py";
        myProcess.StartInfo.UseShellExecute = false;
        myProcess.StartInfo.RedirectStandardInput = true;
        myProcess.StartInfo.RedirectStandardOutput = false;
        myProcess.Start();
    }
}
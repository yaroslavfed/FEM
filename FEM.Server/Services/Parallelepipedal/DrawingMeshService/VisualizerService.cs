using System.Diagnostics;
using FEM.Common.Data.MathModels.MatrixFormats;
using FEM.Server.Data.Parallelepipedal;

namespace FEM.Server.Services.Parallelepipedal.DrawingMeshService;

public class VisualizerService : IVisualizerService
{
    private readonly string _rootPath = Directory.GetCurrentDirectory();
    private readonly string _dataFileName = Path.Combine(Directory.GetCurrentDirectory(), "output.txt");
    private readonly string _scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts\\draw_mesh_script.py");

    public async Task DrawMeshPlotAsync(Mesh mesh)
    {
        await DeleteOutputPlotsAsync();

        var isScriptFileExist = await CheckFilesToAvailabilityAsync(_scriptPath);
        if (!isScriptFileExist)
            throw new FileNotFoundException($"Script file was not found from path {_scriptPath}");

        await ResolveDataToDrawAsync(mesh);

        var isDataFileExist = await CheckFilesToAvailabilityAsync(_dataFileName);
        if (!isDataFileExist)
            throw new FileNotFoundException($"Data file was not found from path {_dataFileName}");

        await RunDrawingAsync();
    }

    public async Task WriteMatrixToFileAsync(IMatrixFormat matrixProfile)
    {
        await DeleteOutputFilesAsync();

        if (matrixProfile is MatrixProfileFormat source)
        {
            Directory.CreateDirectory("OutputProfile");
            await WriteToFileAsync("OutputProfile/Di.txt", source.Di);
            await WriteToFileAsync("OutputProfile/Gg.txt", source.Gg);
            await WriteToFileAsync("OutputProfile/Ig.txt", source.Ig);
            await WriteToFileAsync("OutputProfile/Jg.txt", source.Jg);
            await WriteToFileAsync("OutputProfile/F.txt", source.F);
        }
    }

    private static async Task WriteToFileAsync<T>(string fileName, IList<T> list)
    {
        await using var streamWriter = new StreamWriter(fileName);
        foreach (var item in list)
        {
            switch (item)
            {
                case double:
                    await streamWriter.WriteLineAsync($"{item:0.0000E+00}");
                    break;
                case int:
                    await streamWriter.WriteLineAsync($"{item}");
                    break;
            }
        }
    }

    private Task RunDrawingAsync()
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
            var pointsX = fe
                .Edges
                .SelectMany(edge => edge.Nodes)
                .Select(node => node.Coordinate.X)
                .Distinct()
                .Order()
                .ToList();

            var pointsY = fe
                .Edges
                .SelectMany(edge => edge.Nodes)
                .Select(node => node.Coordinate.Y)
                .Distinct()
                .Order()
                .ToList();

            var pointsZ = fe
                .Edges
                .SelectMany(edge => edge.Nodes)
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

    private static Task<bool> CheckDirectoriesToAvailabilityAsync(string pathToFile) =>
        Task.FromResult(Directory.Exists(pathToFile));

    private async Task DeleteOutputPlotsAsync()
    {
        var outputFilesPath = Path.Combine(_rootPath, "OutputProfile");

        if (await CheckFilesToAvailabilityAsync(outputFilesPath))
            File.Delete(outputFilesPath);
    }

    private async Task DeleteOutputFilesAsync()
    {
        var outputPlotsContentPath = Path.Combine(_rootPath, "output.txt");
        var outputPlotsPath = Path.Combine(_rootPath, "OutputPlots");

        if (await CheckDirectoriesToAvailabilityAsync(outputPlotsContentPath))
            Directory.Delete(outputPlotsContentPath, true);

        if (await CheckDirectoriesToAvailabilityAsync(outputPlotsPath))
            Directory.Delete(outputPlotsPath, true);
    }
}
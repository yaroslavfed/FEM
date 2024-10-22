using System.Diagnostics;
using FEM.Common.DTO.Models.MatrixFormats;
using FEM.Common.DTO.Models.MeshModels;
using FEM.Common.Services.SaverService;

namespace FEM.Common.Services.VisualizerService;

/// <inheritdoc cref="IVisualizerService"/>
public class VisualizerService : IVisualizerService
{
    private readonly ISaverService _saverService;

    private readonly string _rootPath = Directory.GetCurrentDirectory();
    private readonly string _dataFileName = Path.Combine(Directory.GetCurrentDirectory(), "output.txt");
    private readonly string _scriptPath = Path.Combine(Directory.GetCurrentDirectory(), "Scripts\\draw_mesh_script.py");

    /// <inheritdoc cref="VisualizerService" />
    public VisualizerService(ISaverService saverService)
    {
        _saverService = saverService;
    }

    /// <inheritdoc />
    public async Task DrawMeshPlotAsync(Mesh mesh)
    {
        DeleteOutputPlots();

        var isScriptFileExist = CheckFilesToAvailabilityAsync(_scriptPath);
        if (!isScriptFileExist)
            throw new FileNotFoundException($"Script file was not found from path {_scriptPath}");

        await ResolveDataToDrawAsync(mesh);

        var isDataFileExist = CheckFilesToAvailabilityAsync(_dataFileName);
        if (!isDataFileExist)
            throw new FileNotFoundException($"Data file was not found from path {_dataFileName}");

        await RunDrawingAsync();
    }

    /// <inheritdoc />
    public async Task WriteMatrixToFileAsync(MatrixProfileFormat matrixProfile)
    {
        DeleteOutputFiles();

        Directory.CreateDirectory("OutputProfile/");
        await _saverService.WriteListToFileAsync("OutputProfile/Di.txt", matrixProfile.Di);
        await _saverService.WriteListToFileAsync("OutputProfile/Gg.txt", matrixProfile.Gg);
        await _saverService.WriteListToFileAsync("OutputProfile/Ig.txt", matrixProfile.Ig);
        await _saverService.WriteListToFileAsync("OutputProfile/Jg.txt", matrixProfile.Jg);
        await _saverService.WriteListToFileAsync("OutputProfile/F.txt", matrixProfile.F);
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

    private static bool CheckFilesToAvailabilityAsync(string pathToFile) => File.Exists(pathToFile);

    private static bool CheckDirectoriesToAvailabilityAsync(string pathToFile) => Directory.Exists(pathToFile);

    private void DeleteOutputFiles()
    {
        var outputFilesPath = Path.Combine(_rootPath, "OutputProfile/");

        if (CheckDirectoriesToAvailabilityAsync(outputFilesPath))
            Directory.Delete(outputFilesPath, true);
    }

    private void DeleteOutputPlots()
    {
        var outputPlotsContentPath = Path.Combine(_rootPath, "output.txt");
        var outputPlotsPath = Path.Combine(_rootPath, "OutputPlots/");

        if (CheckFilesToAvailabilityAsync(outputPlotsContentPath))
            File.Delete(outputPlotsContentPath);

        if (CheckDirectoriesToAvailabilityAsync(outputPlotsPath))
            Directory.Delete(outputPlotsPath, true);
    }
}
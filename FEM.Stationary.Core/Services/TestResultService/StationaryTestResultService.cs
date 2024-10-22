using System.Diagnostics.CodeAnalysis;
using FEM.Common.Core.Services.TestResultService;
using FEM.Common.DTO.Models.OutputModels;
using FEM.Common.Services.SaverService;
using FEM.Stationary.DTO.TestingContext;

namespace FEM.Stationary.Core.Services.TestResultService;

public class StationaryTestResultService : ITestResultService
{
    private readonly ISaverService _saverService;

    public StationaryTestResultService(ISaverService saverService)
    {
        _saverService = saverService;
    }

    public async Task<Guid> AddTestResultAsync(SolutionResult solutionParameters)
    {
        var plotsRawList = new List<string>();

        var outputPlotsPath = Path.Combine(Directory.GetCurrentDirectory(), "OutputPlots/");
        while (!Directory.Exists(outputPlotsPath))
            await Task.Delay(TimeSpan.FromSeconds(1));

        var dir = new DirectoryInfo(outputPlotsPath);
        var filesList = dir.GetFiles();
        foreach (var file in filesList)
        {
            var imageBytes = await TryToGetImages(file.FullName);
            var base64String = Convert.ToBase64String(imageBytes);
            plotsRawList.Add(base64String);
        }

        var result = new StationaryTestResult
        {
            Id = Guid.NewGuid(),
            SolutionInfo = solutionParameters.SolutionInfo,
            ItersCount = solutionParameters.ItersCount,
            Plots = plotsRawList
        };

        await _saverService.SaveResultAsync(result);

        return result.Id;
    }

    public async Task<string> GetTestResultAsync(Guid id)
    {
        var path = Directory.Exists("TestResults/");

        if (!path)
            throw new DirectoryNotFoundException("Directory with results does not exist");

        var dir = new DirectoryInfo("TestResults/");
        var filesList = dir.GetFiles();
        var file = filesList.FirstOrDefault(info => info.Name.Contains($"{id}"));

        if (file is null)
            throw new FileNotFoundException("File with result does not exist");

        var fileContent = await File.ReadAllTextAsync(file.FullName);
        return fileContent;
    }

    private async Task<byte[]> TryToGetImages(string filePath, int attemptsNumber = 0)
    {
        byte[] result = [];
        var waitingLifetime = TimeSpan.FromSeconds(1);

        try
        {
            if (attemptsNumber < 5)
                result = await File.ReadAllBytesAsync(filePath);
            else
                result = await File.ReadAllBytesAsync(
                    Path.Combine(Directory.GetCurrentDirectory(), "Assets/not-image.png")
                );
        } catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e.Message);
            Console.ResetColor();

            attemptsNumber++;
            await Task.Delay(waitingLifetime);
            await TryToGetImages(filePath, attemptsNumber);
        }

        return result;
    }
}
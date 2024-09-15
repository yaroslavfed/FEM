using FEM.Common.Data.TestSession;
using FEM.Server.Data;
using FEM.Server.Services.SaverService;

namespace FEM.Server.Services.TestResultService;

public class TestResultService : ITestResultService
{
    private readonly ISaverService _saverService;

    public TestResultService(ISaverService saverService)
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
            var imageBytes = await System.IO.File.ReadAllBytesAsync(file.FullName);
            var base64String = Convert.ToBase64String(imageBytes);
            plotsRawList.Add(base64String);
        }

        var result = new TestResult
        {
            Id = Guid.NewGuid(),
            SolutionVector = solutionParameters.Solve.Data,
            Inaccuracy = solutionParameters.Discrepancy, // TODO: заменить на получение погрешности решения задачи
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
            throw new DirectoryNotFoundException("Directory with results doesn`t exist");

        var dir = new DirectoryInfo("TestResults/");
        var filesList = dir.GetFiles();
        var file = filesList.FirstOrDefault(info => info.Name.Contains($"{id}"));

        if (file is null)
            throw new FileNotFoundException("File with result doesn`t exist");

        var fileContent = await File.ReadAllTextAsync(file.FullName);
        return fileContent;
    }
}
using System.Diagnostics;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Client.Shared.Data;

namespace Client.Shared.Services.ReportService;

public class PdfReportService : IReportService
{
    public void GenerateReportAsync(TestResult testResult)
    {
        var document = new Document();
        var page = document.Pages.Add();

        var path = Directory.Exists($"Plots_{testResult.Id}/");

        if (!path)
            throw new DirectoryNotFoundException("Directory with results doesn`t exist");

        var dir = new DirectoryInfo($"Plots_{testResult.Id}/");
        var filesList = dir.GetFiles();
        var files = filesList.Where(info => info.FullName.Contains(".png")).ToArray();

        if (files.Length == 0)
            throw new FileNotFoundException("Files haven`t founded");

        page.Paragraphs.Add(new TextFragment("Расчётная область:"));
        foreach (var plot in files.Select((value, index) => new { index, value }))
        {
            int lowerLeftX = 0 + plot.index * 170;
            int lowerLeftY = 600;

            int upperRightX = 200 + plot.index * 170;
            int upperRightY = 800;

            var imageStream = new FileStream($"{plot.value.FullName}", FileMode.Open);
            page.Resources.Images.Add(imageStream);
            page.Contents.Add(new Aspose.Pdf.Operators.GSave());
            var rectangle = new Rectangle(lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            var matrix = new Matrix(
                [
                    rectangle.URX - rectangle.LLX,
                    0,
                    0,
                    rectangle.URY - rectangle.LLY,
                    rectangle.LLX,
                    rectangle.LLY
                ]
            );

            page.Contents.Add(new Aspose.Pdf.Operators.ConcatenateMatrix(matrix));
            XImage ximage = page.Resources.Images[page.Resources.Images.Count];

            page.Contents.Add(new Aspose.Pdf.Operators.Do(ximage.Name));
            page.Contents.Add(new Aspose.Pdf.Operators.GRestore());
        }

        page.Paragraphs.Add(new TextFragment("\n"));
        page.Paragraphs.Add(new TextFragment("\n"));
        page.Paragraphs.Add(new TextFragment("\n"));
        page.Paragraphs.Add(new TextFragment("\n"));
        page.Paragraphs.Add(new TextFragment("\n"));
        page.Paragraphs.Add(new TextFragment("\n"));
        page.Paragraphs.Add(new TextFragment("\n"));
        page.Paragraphs.Add(new TextFragment($"Точность решения: {testResult.SolutionInfo!.Discrepancy:E}"));
        page.Paragraphs.Add(new TextFragment($"Кол-во итераций: {testResult.ItersCount}"));
        page.Paragraphs.Add(new TextFragment("Вектор решения:"));
        page.Paragraphs.Add(new TextFragment("\n"));
        foreach (var point in testResult.SolutionInfo!.EdgeVectorValue)
        {
            page.Paragraphs.Add(new TextFragment($"{point}"));
        }

        document.Save($"document_{testResult.Id}.pdf");

        Process.Start(new ProcessStartInfo { FileName = $"document_{testResult.Id}.pdf", UseShellExecute = true });
    }
}
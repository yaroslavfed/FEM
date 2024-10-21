using FEM.Common.Data.MeshModels;
using FEM.Common.Data.TestingContext;
using FEM.NonStationary.DTO.TestingContext;
using FEM.Server.Data;
using FEM.Server.Data.OutputModels;
using FEM.Server.Services.TestingService;

namespace FEM.Server.Services.InaccuracyService;

public class InaccuracyService : IInaccuracyService
{
    private readonly IProblemService _problemService;

    public InaccuracyService(IProblemService problemService)
    {
        _problemService = problemService;
    }

    public async Task GetSolutionVectorInaccuracy(NonStationaryTestSession<Mesh> nonStationaryTestSession, SolutionResult solutionResult)
    {
        var edgeVectorTruthValue = new List<double>();

        var edgesList = nonStationaryTestSession
                        .Mesh
                        .Elements
                        .SelectMany(element => element.Edges)
                        .DistinctBy(edge => edge.EdgeIndex)
                        .OrderBy(edge => edge.EdgeIndex)
                        .ToList();

        foreach (var edge in edgesList)
        {
            var localNodes = await _problemService.ResolveLocalNodes(edge, nonStationaryTestSession);
            var matrixContributions = await _problemService.ResolveMatrixContributionsAsync(
                (localNodes.firstNode, localNodes.secondNode),
                localNodes.direction
            );

            edgeVectorTruthValue.Add(matrixContributions);
        }

        var inaccuracyVector = new List<double>();

        for (var i = 0; i < solutionResult.Solve.Data.Count; i++)
            inaccuracyVector.Add(Math.Abs(solutionResult.Solve.Data[i] - edgeVectorTruthValue[i]));

        var norm = 0.0;
        foreach (var item in inaccuracyVector)
            norm += Math.Pow(item, 2);

        var normTruth = 0.0;
        for (var i = 0; i < inaccuracyVector.Count; i++)
            normTruth += Math.Pow(edgeVectorTruthValue[i], 2);

        var solutionInaccuracy = new SolutionAdditionalInfo { Discrepancy = Math.Sqrt(norm) / Math.Sqrt(normTruth) };

        for (var i = 0; i < solutionResult.Solve.Data.Count; i++)
        {
            solutionInaccuracy.EdgeNumber.Add(i);
            solutionInaccuracy.EdgeVectorValue.Add(solutionResult.Solve.Data[i]);
            solutionInaccuracy.EdgeVectorTruthValue.Add(edgeVectorTruthValue[i]);
            solutionInaccuracy.Inaccuracy.Add(inaccuracyVector[i]);
        }

        solutionResult.SolutionInfo = solutionInaccuracy;
    }
}
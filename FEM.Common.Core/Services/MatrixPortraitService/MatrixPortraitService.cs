using FEM.Common.DTO.Models.MatrixFormats;
using FEM.Common.DTO.Models.MeshModels;

namespace FEM.Common.Core.Services.MatrixPortraitService;

/// <inheritdoc cref="IMatrixPortraitService"/>
public class MatrixPortraitService : IMatrixPortraitService
{
    /// <inheritdoc cref="IMatrixPortraitService.ResolveMatrixPortraitAsync"/>
    public async Task<MatrixProfileFormat> ResolveMatrixPortraitAsync(Mesh mesh)
    {
        var edgesCount = mesh.Elements.SelectMany(element => element.Edges).DistinctBy(edge => edge.EdgeIndex).Count();
        var bufferList = Enumerable.Range(0, edgesCount).Select(_ => new List<int>()).ToList();

        foreach (var finiteElement in mesh.Elements)
        {
            var bufferEdge = finiteElement.Edges.Select(edge => edge.EdgeIndex).Order().ToList();
            for (var i = 0; i < bufferEdge.Count - 1; i++)
            {
                for (var j = i + 1; j < bufferEdge.Count; j++)
                {
                    var isExist = false;
                    var positionIndex = bufferEdge[j];
                    var elementIndex = bufferEdge[i];

                    for (var k = 0; k < bufferList[positionIndex].Count; k++)
                        if (elementIndex == bufferList[positionIndex][k])
                            isExist = true;

                    if (!isExist)
                        bufferList[positionIndex].Add(elementIndex);
                }
            }

            bufferEdge.Clear();
        }

        foreach (var item in bufferList.Where(item => !IsOrdered(item)))
            item.Sort();

        var matrixProfile = new MatrixProfileFormat();

        return await matrixProfile.CreateProfileArraysAsync(bufferList);
    }

    /// <summary>
    /// Проверка упорядоченности списков
    /// </summary>
    /// <param name="list">Проверяемый список</param>
    private static bool IsOrdered(IList<int> list)
    {
        if (!list.Any())
            return true;

        for (var i = 0; i < list.Count - 1; i++)
            if (list[i + 1] < list[i])
                return false;

        return true;
    }
}
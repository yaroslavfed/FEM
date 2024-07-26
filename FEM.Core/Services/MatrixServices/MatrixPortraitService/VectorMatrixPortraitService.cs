using FEM.Common.Data.Domain;
using FEM.Core.Data.Dto.MatrixFormat;

namespace FEM.Core.Services.MatrixServices.MatrixPortraitService;

/// <inheritdoc cref="IMatrixPortraitService{TMesh,TMatrixFormat}"/>
public class VectorMatrixPortraitService<TMesh, TMatrixFormat> : IMatrixPortraitService<TMesh, TMatrixFormat>
    where TMesh : Mesh
    where TMatrixFormat : MatrixProfileFormat, new()
{
    /// <inheritdoc cref="IMatrixPortraitService{TMesh,TMatrixFormat}.ResolveMatrixPortrait"/>
    public async Task<TMatrixFormat> ResolveMatrixPortrait(TMesh mesh)
    {
        var edgesCount = mesh.Elements.SelectMany(element => element.Edges).DistinctBy(edge => edge.EdgeIndex).Count();
        var bufferList = Enumerable.Range(0, edgesCount).Select(item => new List<int>()).ToList();

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

        return await CreateProfileArraysAsync(bufferList);
    }

    /// <summary>
    /// Собираем портрет матрицы
    /// </summary>
    private Task<TMatrixFormat> CreateProfileArraysAsync(List<List<int>> bufferList)
    {
        var matrix = new TMatrixFormat();

        matrix.Ig = [0, 0, ..Enumerable.Range(0, bufferList.Count - 1).Select(item => 0)];
        for (var i = 1; i < bufferList.Count; i++)
            matrix.Ig[i + 1] = matrix.Ig[i] + bufferList[i].Count;

        matrix.Jg = [..Enumerable.Range(0, matrix.Ig.Last()).Select(item => 0)];
        for (int i = 1, j = 0; i < bufferList.Count; i++)
            foreach (var bufferItem in bufferList[i])
                matrix.Jg[j++] = bufferItem;

        return Task.FromResult(matrix);
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
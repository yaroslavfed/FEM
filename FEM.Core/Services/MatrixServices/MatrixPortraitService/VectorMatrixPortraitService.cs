using FEM.Common.Data.Domain;

namespace FEM.Core.Services.MatrixServices.MatrixPortraitService;

public class VectorMatrixPortraitService<TMesh> : IMatrixPortraitService<TMesh> where TMesh : Mesh
{
    public Task ResolveMatrixPortrait(TMesh mesh)
    {
        var edgesCount = mesh.Elements.SelectMany(element => element.Edges).DistinctBy(edge => edge.EdgeIndex).Count();
        List<List<int>> bufferList = Enumerable.Range(0, edgesCount).Select(item => new List<int>()).ToList();

        foreach (var finiteElement in mesh.Elements)
        {
            var bufferEdge = finiteElement.Edges.Select(edge => edge.EdgeIndex).Order().ToList();
            for (var i = 0; i < bufferEdge.Count - 1; i++)
            {
                for (var j = 0; j < bufferEdge.Count - 1; j++)
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

        // TODO: добавить формирование массивов матрицы

        return Task.CompletedTask;
    }

    private static bool IsOrdered(in IList<int> list)
    {
        if (!list.Any())
            return true;

        for (var i = 0; i < list.Count - 1; i++)
            if (list[i + i] < list[i])
                return false;

        return true;
    }
}
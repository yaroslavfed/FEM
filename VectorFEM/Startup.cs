using VectorFEM.Data;
using VectorFEM.Enums;
using VectorFEM.Services;

namespace VectorFEM;

public class Startup(IGlobalMatrixServices globalMatrixServices)
{
    private const double GAMMA = 1;
    private const double MU = 1;

    public Task Run()
    {
        var element = new FiniteElement(
            X0: -1, Xn: 1, Y0: -1, Yn: 1, Z0: -1, Zn: 1,
            Edges: Enumerable.Range(1, 12).Select(i => new Edge(i)).ToList());

        var globalMatrix = globalMatrixServices.GetGlobalMatrix(mu: MU, gamma: GAMMA, element: element, EFemType.Vector)
            .ToString();

        Console.WriteLine(globalMatrix);

        return Task.CompletedTask;
    }
}
using VectorFEM.Data;
using VectorFEM.Services.MassMatrixResolver;

namespace VectorFEM;

public class Startup(IMassMatrixResolver<Matrix> massMatrixResolver)
{
    public Task Run()
    {
        var position = new Sensor(0, 0.5, 0);

        var element = new FiniteElement(
            X0: -1, Xn: 1, Y0: -1, Yn: 1, Z0: -1, Zn: 1,
            Edges: Enumerable.Range(1, 12).Select(i => new Edge(i)).ToList());

        Console.WriteLine(massMatrixResolver.ResolveMassMatrixStrategy(EFemType.Vector)
            .GetMassMatrix(element, position).ToString());

        return Task.CompletedTask;
    }
}
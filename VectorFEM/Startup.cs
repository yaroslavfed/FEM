using VectorFEM.Data;
using VectorFEM.Enums;
using VectorFEM.Services.MassMatrixResolver;

namespace VectorFEM;

public class Startup(IMassMatrixResolver<Matrix> massMatrixResolver)
{
    private const double _gamma = 1;
    private const double _mu = 1;

    public Task Run()
    {
        var position = new Sensor(0, 0.5, 0);

        var element = new FiniteElement(
            X0: -1, Xn: 1, Y0: -1, Yn: 1, Z0: -1, Zn: 1,
            Edges: Enumerable.Range(1, 12).Select(i => new Edge(i)).ToList());

        var massMatrix = massMatrixResolver.ResolveMassMatrixStrategy(EFemType.Vector);
        Console.WriteLine(massMatrix.GetMassMatrix(_gamma, position).ToString());

        return Task.CompletedTask;
    }
}
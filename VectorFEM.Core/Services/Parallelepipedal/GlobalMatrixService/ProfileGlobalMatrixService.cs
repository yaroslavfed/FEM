using FEM.Common.Data.MathModels;
using FEM.Common.Data.MathModels.MatrixFormats;
using VectorFEM.Core.Models.Parallelepipedal.MassMatrix;
using VectorFEM.Core.Models.Parallelepipedal.StiffnessMatrix;
using VectorFEM.Core.Services.Parallelepipedal.MatrixPortraitService;
using VectorFEM.Core.Services.Parallelepipedal.MeshService;
using VectorFEM.Core.Services.TestSessionService;

namespace VectorFEM.Core.Services.Parallelepipedal.GlobalMatrixService;

public class ProfileGlobalMatrixService<TMatrixProfile> : IGlobalMatrixServices<TMatrixProfile>
    where TMatrixProfile : MatrixProfileFormat, new()
{
    private readonly IMatrixPortraitService<TMatrixProfile> _portraitService;
    private readonly IMeshService                           _meshService;
    private readonly ITestSessionService                    _testSessionService;
    private readonly IStiffnessMatrix<Matrix>               _stiffnessMatrixResolver;
    private readonly IMassMatrix<Matrix>                    _massMatrixResolver;

    public ProfileGlobalMatrixService(
        IMeshService meshService,
        IMatrixPortraitService<TMatrixProfile> portraitService,
        ITestSessionService testSessionService,
        IStiffnessMatrix<Matrix> stiffnessMatrixResolver,
        IMassMatrix<Matrix> massMatrixResolver
    )
    {
        _testSessionService = testSessionService;
        _stiffnessMatrixResolver = stiffnessMatrixResolver;
        _massMatrixResolver = massMatrixResolver;
        _portraitService = portraitService;
        _meshService = meshService;
    }

    public async Task<TMatrixProfile> GetGlobalMatrixAsync()
    {
        var testSession = await _testSessionService.CreateTestSessionAsync();
        var mesh = await _meshService.GenerateMeshAsync();
        var matrixProfile = await _portraitService.ResolveMatrixPortraitAsync(mesh);

        foreach (var element in mesh.Elements)
        {
            // var firstNode = (from edge in mesh.Elements[elementIndex].Edges
            //                  from node in edge.Nodes
            //                  where node.NodeIndex == 0
            //                  select node.Coordinate)
            //                 .ToArray()
            //                 .Single();
            //
            // var secondNode = (from edge in mesh.Elements[elementIndex].Edges
            //                   from node in edge.Nodes
            //                   where node.NodeIndex == 7
            //                   select node.Coordinate)
            //                  .ToArray()
            //                  .Single();
            //
            // var hx = secondNode.X - firstNode.X;
            // var hy = secondNode.Y - firstNode.Y;
            // var hz = secondNode.Z - firstNode.Z;

            // var massMatrix = _massMatrixResolver
            //                  .ResolveMassMatrixStrategy(element, EFemType.Vector)
            //                  .GetMassMatrix(testSession.Gamma);
            // var stiffnessMatrix = _stiffnessMatrixResolver
            //                       .ResolveStiffnessMatrixStrategy(element, EFemType.Vector)
            //                       .GetStiffnessMatrix(testSession.Mu);

            // getLocalRightPart(firstNode, hx, hy, hz, elementIndex);
            // for (int i = 0; i < finite_elems[i].edges.size(); i++)
            // {
            //     for (int j = 0; j < finite_elems[i].edges.size(); j++)
            //     {
            //         addElementToGlobal(finite_elems[i].edges[i], finite_elems[i].edges[j], stiffness_loc[i][j]);
            //         addElementToGlobal(finite_elems[i].edges[i], finite_elems[i].edges[j], mass_loc[i][j]);
            //     }
            //
            //     F[finite_elems[i].edges[i]] += function_loc[i];
            // }
        }

        return new TMatrixProfile();
    }

    // private IList<double> ResolveLocalRightPart(
    //     Point3D point,
    //     double hx,
    //     double hy,
    //     double hz,
    //     int elementId,
    //     TMesh mesh
    // )
    // {
    //     var localRightPart = new List<double>();
    //     
    //     for (int i = 0; i < localRightPart.Count; i++)
    //         localRightPart.Add(RP_value(mesh.Elements[elementId].Edges[i]));
    //     
    //     double coefficient = hx * hy * hz / 36.0;
    //     
    //     for (int i = 0; i < M.size(); i++)
    //         for (int j = 0; j < M.size(); j++)
    //             localRightPart[i] += coefficient * M[i][j] * localRightPart[j];
    // }
}
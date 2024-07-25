namespace FEM.Core.Services.MatrixServices.MatrixPortraitService;

public interface IMatrixPortraitService<in TMesh>
{
    Task ResolveMatrixPortrait(TMesh mesh);
}
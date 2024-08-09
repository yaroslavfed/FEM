using FEM.Common.Data.Domain;
using VectorFEM.Core.Data.Parallelepipedal;

namespace VectorFEM.Core.Services.Parallelepipedal.RightPartVectorService;

/// <summary>
/// ������ ���������� ������� ������ �����
/// </summary>
public interface IRightPartVectorService
{
    /// <summary>
    /// ������ �������� ��������� ������� ������ �����
    /// </summary>
    Task<double> ResolveRightPartValueAsync(Edge edge, Mesh strata);
}
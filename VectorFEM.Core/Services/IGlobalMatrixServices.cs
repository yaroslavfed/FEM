using VectorFEM.Core.Enums;
using VectorFEM.Data;

namespace VectorFEM.Core.Services;

public interface IGlobalMatrixServices
{
    Matrix GetGlobalMatrix(double mu, double gamma, FiniteElement element, EFemType femType);
}